using OpenCvSharp;
using OpenCvSharp.Internal.Vectors;
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

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //�ļ�·������
            _imgPath = txtImgPath.Text;

            // ��ȡͼ���ļ�
            _image = Cv2.ImRead(_imgPath, ImreadModes.Color);

            // ���ͼ���ȡʧ�ܣ���ӡ������Ϣ���˳�
            if (_image.Empty())
            {
                Serilog.Log.Warning("�޷���ȡͼ���ļ�");
                return;
            }

            //��ʾԭʼ��Ƭ
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_image), 1);

            // ��ͼ��ת��Ϊ HSV ��ɫ�ռ�
            _hsvImage = new Mat();
            Cv2.CvtColor(_image, _hsvImage, ColorConversionCodes.BGR2HSV);
        }

        private void btnFushi_Click(object sender, EventArgs e)
        {
            // ִ�и�ʴ����
            Cv2.Erode(_image, _hsvImage, _kernel);

            //��ʾ��Ƭ
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_hsvImage), 1);
        }

        private void btnPengzhang_Click(object sender, EventArgs e)
        {
            // ִ�����Ͳ���
            Cv2.Dilate(_hsvImage, _hsvImage, _kernel);

            //��ʾ��Ƭ
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_hsvImage), 1);
        }

        private void btnOcr_Click(object sender, EventArgs e)
        {
            _imgPath = txtImgPath.Text;
            Task.Run(Ocr);
        }


        private void Ocr()
        {
            // ��ȡͼ���ļ�
            Mat image = Cv2.ImRead(_imgPath, ImreadModes.Color);

            // ���ͼ���ȡʧ�ܣ���ӡ������Ϣ���˳�
            if (image.Empty())
            {
                Serilog.Log.Warning("�޷���ȡͼ���ļ�");
                return;
            }

            //��ʾԭʼ��Ƭ
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 1);

            // ��ͼ��ת��Ϊ HSV ��ɫ�ռ�
            Mat hsvImage = new Mat();
            Cv2.CvtColor(image, hsvImage, ColorConversionCodes.BGR2HSV);

            // �������ͺ͸�ʴ���ں�
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(10, 10));

            // ִ�и�ʴ����
            Mat erodedImage = new Mat();
            Cv2.Erode(hsvImage, erodedImage, kernel);

            //ģ������
            OpenCvSharp.Size ksize = new OpenCvSharp.Size(5, 5);
            OpenCvSharp.Point anchor = new OpenCvSharp.Point(3, 3);
            BorderTypes borderType = BorderTypes.Constant;
            //Cv2.Blur(mInput, blur, ksize, anchor, borderType);  //ģ��
            Cv2.GaussianBlur(erodedImage, image, ksize, 0);  //��˹ģ��

            // ִ�����Ͳ���
            Mat dilatedImage = new Mat();
            Cv2.Dilate(image, dilatedImage, kernel);

            // �����ɫ��Χ�����޺�����һ
            Scalar lowerRed = new Scalar(0, 43, 46);
            Scalar upperRed = new Scalar(10, 255, 255);
            Mat mask = new Mat();
            Cv2.InRange(dilatedImage, lowerRed, upperRed, mask);
            Mat redRegion = new Mat();
            Cv2.BitwiseAnd(dilatedImage, dilatedImage, redRegion, mask);

            //Cv2.ImWrite(Path.Combine(_saveFolder, "mask.jpg"), mask);
            //Cv2.ImWrite(Path.Combine(_saveFolder, "region.jpg"), redRegion);

            // �����ɫ��Χ�����޺�����һ              
            Scalar lowerRed2 = new Scalar(156, 43, 46);
            Scalar upperRed2 = new Scalar(180, 255, 255);
            Mat mask2 = new Mat();
            Cv2.InRange(dilatedImage, lowerRed2, upperRed2, mask2);
            Mat redRegion2 = new Mat();
            Cv2.BitwiseAnd(dilatedImage, dilatedImage, redRegion2, mask2);

            //Cv2.ImWrite(Path.Combine(_saveFolder, "mask2.jpg"), mask2);
            //Cv2.ImWrite(Path.Combine(_saveFolder, "region2.jpg"), redRegion2);

            Mat mask3 = mask + mask2;
            //Cv2.ImWrite(Path.Combine(_saveFolder, "mask3.jpg"), mask3);

            //��ʾͼƬ2
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mask3), 2);

            // Ѱ�Һ�ɫ����ı߽��
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(mask3, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // ���ƾ��ο�,���и��
            foreach (var contour in contours)
            {
                // ��ȡ�����ı߽��
                Rect boundingRect = Cv2.BoundingRect(contour);
                Serilog.Log.Information("�и������С��w:{0} h:{1}", boundingRect.Width, boundingRect.Height);

                if (boundingRect.Width < 10 || boundingRect.Height < 10)
                {
                    Serilog.Log.Information("�����С�����Բü�");
                    continue;
                }

                // �и��ɫ����
                //Mat redRegionCropped = new Mat(image, boundingRect);

                // �����и��ĺ�ɫ����Ϊ�µ�ͼ���ļ�
                //Cv2.ImWrite(Path.Combine(_saveFolder, $"{DateTime.Now.Ticks}.jpg"), redRegionCropped);

                // ���ƾ���
                Cv2.Rectangle(image, boundingRect, Scalar.Red, 2);
            }



            //Cv2.ImWrite(Path.Combine(_saveFolder, "erodedImage.jpg"), erodedImage);

            //��ʾͼƬ3
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 3);

            image.Dispose();

            Serilog.Log.Information("ʶ�����");
        }

        private void Contours()
        {
            // ��ȡͼ���ļ�
            Mat image = Cv2.ImRead(_imgPath, ImreadModes.Color);

            // ���ͼ���ȡʧ�ܣ���ӡ������Ϣ���˳�
            if (image.Empty())
            {
                Serilog.Log.Warning("�޷���ȡͼ���ļ�");
                return;
            }

            //��ʾԭʼ��Ƭ
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 1);

            // ת��ͼ��Ϊ�Ҷ�
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            // Ӧ����ֵ���κ�����Ԥ�����Ի�ȡ������ͼ�������Ҫ��
            Mat binary = new Mat();
            Cv2.Threshold(gray, binary, 127, 255, ThresholdTypes.Binary);

            // ��������
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // ��������
            foreach (var contour in contours)
            {
                // ��ȡ�߽���Σ����ƾ���
                Rect boundingRect = Cv2.BoundingRect(contour);
                Cv2.Rectangle(image, boundingRect, Scalar.Red, 2);

                // ��ȡ��С���Բ
                Cv2.MinEnclosingCircle(contour, out Point2f center, out float radius);
                Cv2.Circle(image, (int)center.X, (int)center.Y, (int)radius, Scalar.Yellow); // ����Բ
            }

            //��ʾͼƬ
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 2);
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
