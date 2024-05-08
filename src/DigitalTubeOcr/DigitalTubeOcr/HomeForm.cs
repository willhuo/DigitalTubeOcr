using OpenCvSharp;
using OpenCvSharp.Internal.Vectors;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DigitalTubeOcr
{
    public partial class HomeForm : Form
    {
        private string _imgPath { get; set; }
        private string _saveFolder = "data";
        private Mat _image { get; set; }
        private Mat _hsvImage { get; set; }
        private Mat _kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(10, 10));


        public HomeForm()
        {
            InitializeComponent();
        }

        private void HomeForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(_saveFolder))
                Directory.CreateDirectory(_saveFolder);

            Dijing.SerilogExt.InMemorySink.OnLogReceivedEvent += InMemorySink_OnLogReceivedEvent;
            Dijing.SerilogExt.InitLog.SetLog(Dijing.SerilogExt.RunModeEnum.Debug);
            Serilog.Log.Information("OCR���������ɹ�");
        }

        private void btnOcr_Click(object sender, EventArgs e)
        {
            _imgPath = txtImgPath.Text;
            TemperatureRoiCut(_imgPath);
        }


        private void TemperatureRoiCut(string path)
        {
            //ͼ���ַ
            var imgPath = string.IsNullOrEmpty(path) ? "C:\\willhuo\\DigitalTubeOcr\\doc\\3.jpg" : path;
            var imgName = Path.GetFileNameWithoutExtension(imgPath);

            // ��ȡͼ���ļ�
            Mat image = Cv2.ImRead(imgPath, ImreadModes.Color);

            // ���ͼ���ȡʧ�ܣ���ӡ������Ϣ���˳�
            if (image.Empty())
            {
                Serilog.Log.Warning("�޷���ȡͼ���ļ�");
                return;
            }

            //Blur������ͼ������˲�
            Mat blurImage = new Mat();
            Cv2.Blur(image, blurImage, new OpenCvSharp.Size(10, 10));
            Serilog.Log.Information("�˲����");

            //����ԭʼͼƬ���˲�ͼƬ
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_image.jpg"), image);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_blurImage.jpg"), blurImage);

            // ��ͼ��ת��Ϊ HSV ��ɫ�ռ�
            Mat hsvImage = new Mat();
            Cv2.CvtColor(blurImage, hsvImage, ColorConversionCodes.BGR2HSV);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_hsvImage.jpg"), hsvImage);

            // �����ɫ�� HSV ��Χ
            //Scalar lowerRed = new Scalar(0, 100, 100);
            //Scalar upperRed = new Scalar(10, 255, 255);
            Scalar lowerRed = new Scalar(0, 43, 46);
            Scalar upperRed = new Scalar(10, 255, 255);

            // �ҵ���ɫ����
            Mat mask1 = new Mat();
            Cv2.InRange(hsvImage, lowerRed, upperRed, mask1);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_mask1.jpg"), mask1);

            // �����ɫ�� HSV ��Χ2
            //Scalar lowerRed2 = new Scalar(160, 100, 100);
            //Scalar upperRed2 = new Scalar(179, 255, 255);
            Scalar lowerRed2 = new Scalar(156, 43, 46);
            Scalar upperRed2 = new Scalar(180, 255, 255);

            // �ҵ���ɫ����2
            Mat mask2 = new Mat();
            Cv2.InRange(hsvImage, lowerRed2, upperRed2, mask2);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_mask2.jpg"), mask2);

            // �ϲ�������ɫ����� mask
            Mat mask = new Mat();
            Cv2.Add(mask1, mask2, mask);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_mask.jpg"), mask);

            // ��Ǻ�ɫ����
            Mat redRegions = new Mat();
            Cv2.BitwiseAnd(image, image, redRegions, mask);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_redRegions.jpg"), redRegions);

            // ���Һ�ɫ���������
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(mask, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxSimple);
            Serilog.Log.Information("contours len={0}, hierarchy len={1}", contours.Length, hierarchy.Length);

            // ���˳�����������δ�С������
            double maxAspectRatio = 2.0; // �������Ŀ�߱�
            double minAspectRatio = 0.5; // ������С�Ŀ�߱�

            //���˺�ĵ㼯
            var leftContours = new List<OpenCvSharp.Point[]>();

            // ��ԭʼͼ���ϻ�������
            int rectCount = 0;
            Scalar color = new Scalar(0, 255, 0);
            Mat image2 = image.Clone();
            for (int i = 0; i < contours.Length; i++)
            {
                // �������
                double area = Cv2.ContourArea(contours[i]);                

                //�ų����С������
                if (area <= 500)
                {
                    continue;
                }

                //����и�������Ҳ����
                if (hierarchy[i].Parent >= 0)
                {
                    continue;
                }

                // ��ȡ�����ı߽��
                Rect boundingRect = Cv2.BoundingRect(contours[i]);

                // ����߽��Ŀ�Ⱥ͸߶�
                double aspectRatio = (double)boundingRect.Width / boundingRect.Height;

                // �����߱���һ����Χ�ڣ�����Ϊ�Ǵ���������δ�С������
                if (aspectRatio < minAspectRatio || aspectRatio > maxAspectRatio)
                {
                    continue;
                }

                //������Ϣ
                Serilog.Log.Information("������Ϣ��{0},{1}", i, JsonSerializer.Serialize(hierarchy[i]));
                Serilog.Log.Information("������w:{0} h:{1} aspectRatio={2}, xarea={3}, area={4}", boundingRect.Width, boundingRect.Height, aspectRatio, boundingRect.Width * boundingRect.Height, area);

                //ԭʼͼ�񻭿�
                rectCount++;
                Cv2.DrawContours(image2, contours, i, color, thickness: 2, hierarchy: hierarchy);

                //���˺�ĵ㼯׷�ӵ�����
                leftContours.Add(contours[i]);
            }
            Serilog.Log.Information("���˺������������{0}", rectCount);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_image_rect_{rectCount}.jpg"), image2);

            //�������������Ƿ���1�������������2����ִ�кϲ���,���򱨴�
            if(leftContours.Count>2)
            {
                Serilog.Log.Warning("�¶���������ʶ��ʧ��");
                return;
            }

            //�߽�ϲ���rect
            List<Rect> boundingRects = new List<Rect>();
            foreach (var contour in leftContours)
            {
                Rect rect = Cv2.BoundingRect(contour);
                boundingRects.Add(rect);
            }
            Rect mergedRect = MergeRectangles(boundingRects);

            //�ϲ���Ĵ���������
            Mat image3 = image.Clone();
            Cv2.Rectangle(image3, mergedRect, color, 2);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_mergedimage.jpg"), image3);

            //�ü����¶�����СͼƬ
            Mat roi = new Mat(image, mergedRect);
            Cv2.ImWrite(Path.Combine(_saveFolder, $"{imgName}_roi.jpg"), roi);

            //����
            image.Dispose();
            image2.Dispose();
            image3.Dispose();
            blurImage.Dispose();
            hsvImage.Dispose();
            mask1.Dispose();
            mask2.Dispose();
            mask.Dispose();
            redRegions.Dispose();
            roi.Dispose();
            Serilog.Log.Information("����");
        }

        private void Batch()
        {
            var fileList = new List<string>();
            fileList.Add("C:\\willhuo\\DigitalTubeOcr\\doc\\1.jpg");
            fileList.Add("C:\\willhuo\\DigitalTubeOcr\\doc\\2.jpg");
            fileList.Add("C:\\willhuo\\DigitalTubeOcr\\doc\\3.jpg");
            fileList.Add("C:\\willhuo\\DigitalTubeOcr\\doc\\4.jpg");
            fileList.Add("C:\\willhuo\\DigitalTubeOcr\\doc\\5.jpg");

            foreach(var v in fileList)
            {
                TemperatureRoiCut(v);
            }
        }

        
        private Rect MergeRectangles(List<Rect> rects)
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (var rect in rects)
            {
                if (rect.X < minX)
                    minX = rect.X;
                if (rect.Y < minY)
                    minY = rect.Y;
                if (rect.X + rect.Width > maxX)
                    maxX = rect.X + rect.Width;
                if (rect.Y + rect.Height > maxY)
                    maxY = rect.Y + rect.Height;
            }

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        private void ShowImage(Bitmap img, int location)
        {
            switch (location)
            {
                case 1:
                    {
                        pic1.Invoke(new Action(() =>
                        {
                            pic1.Image = img;
                        }));
                    }
                    break;
                case 2:
                    {
                        pic2.Invoke(new Action(() =>
                        {
                            pic2.Image = img;
                        }));
                    }
                    break;
                case 3:
                    {
                        pic3.Invoke(new Action(() =>
                        {
                            pic3.Image = img;
                        }));
                    }
                    break;
            }
        }


        private void txtImgPath_MouseDown(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Title = @"��ѡ��Ҫ������ļ�",
                RestoreDirectory = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                txtImgPath.Text = filePath;
            }
        }

        private void InMemorySink_OnLogReceivedEvent(object? sender, string e)
        {
            try
            {
                txtLog.Invoke(new Action(() =>
                {
                    if (txtLog.Lines.Length > 10000)
                        txtLog.ResetText();

                    txtLog.AppendText(e + Environment.NewLine);
                    txtLog.ScrollToCaret();
                }));
            }
            catch
            {
                // ignored
            }
        }
    }
}
