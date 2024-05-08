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
            Serilog.Log.Information("OCR程序启动成功");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //文件路径载入
            _imgPath = txtImgPath.Text;

            // 读取图像文件
            _image = Cv2.ImRead(_imgPath, ImreadModes.Color);

            // 如果图像读取失败，打印错误消息并退出
            if (_image.Empty())
            {
                Serilog.Log.Warning("无法读取图像文件");
                return;
            }

            //显示原始照片
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_image), 1);

            // 将图像转换为 HSV 颜色空间
            _hsvImage = new Mat();
            Cv2.CvtColor(_image, _hsvImage, ColorConversionCodes.BGR2HSV);
        }

        private void btnFushi_Click(object sender, EventArgs e)
        {
            // 执行腐蚀操作
            Cv2.Erode(_image, _hsvImage, _kernel);

            //显示照片
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_hsvImage), 1);
        }

        private void btnPengzhang_Click(object sender, EventArgs e)
        {
            // 执行膨胀操作
            Cv2.Dilate(_hsvImage, _hsvImage, _kernel);

            //显示照片
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(_hsvImage), 1);
        }

        private void btnOcr_Click(object sender, EventArgs e)
        {
            _imgPath = txtImgPath.Text;
            Task.Run(Ocr);
        }


        private void Ocr()
        {
            // 读取图像文件
            Mat image = Cv2.ImRead(_imgPath, ImreadModes.Color);

            // 如果图像读取失败，打印错误消息并退出
            if (image.Empty())
            {
                Serilog.Log.Warning("无法读取图像文件");
                return;
            }

            //显示原始照片
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 1);

            // 将图像转换为 HSV 颜色空间
            Mat hsvImage = new Mat();
            Cv2.CvtColor(image, hsvImage, ColorConversionCodes.BGR2HSV);

            // 定义膨胀和腐蚀的内核
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(10, 10));

            // 执行腐蚀操作
            Mat erodedImage = new Mat();
            Cv2.Erode(hsvImage, erodedImage, kernel);

            //模糊操作
            OpenCvSharp.Size ksize = new OpenCvSharp.Size(5, 5);
            OpenCvSharp.Point anchor = new OpenCvSharp.Point(3, 3);
            BorderTypes borderType = BorderTypes.Constant;
            //Cv2.Blur(mInput, blur, ksize, anchor, borderType);  //模糊
            Cv2.GaussianBlur(erodedImage, image, ksize, 0);  //高斯模糊

            // 执行膨胀操作
            Mat dilatedImage = new Mat();
            Cv2.Dilate(image, dilatedImage, kernel);

            // 定义红色范围的下限和上限一
            Scalar lowerRed = new Scalar(0, 43, 46);
            Scalar upperRed = new Scalar(10, 255, 255);
            Mat mask = new Mat();
            Cv2.InRange(dilatedImage, lowerRed, upperRed, mask);
            Mat redRegion = new Mat();
            Cv2.BitwiseAnd(dilatedImage, dilatedImage, redRegion, mask);

            //Cv2.ImWrite(Path.Combine(_saveFolder, "mask.jpg"), mask);
            //Cv2.ImWrite(Path.Combine(_saveFolder, "region.jpg"), redRegion);

            // 定义红色范围的下限和上限一              
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

            //显示图片2
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mask3), 2);

            // 寻找红色区域的边界框
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(mask3, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // 绘制矩形框,并切割保存
            foreach (var contour in contours)
            {
                // 获取轮廓的边界框
                Rect boundingRect = Cv2.BoundingRect(contour);
                Serilog.Log.Information("切割区域大小：w:{0} h:{1}", boundingRect.Width, boundingRect.Height);

                if (boundingRect.Width < 10 || boundingRect.Height < 10)
                {
                    Serilog.Log.Information("区域过小，忽略裁剪");
                    continue;
                }

                // 切割红色区域
                //Mat redRegionCropped = new Mat(image, boundingRect);

                // 保存切割后的红色区域为新的图像文件
                //Cv2.ImWrite(Path.Combine(_saveFolder, $"{DateTime.Now.Ticks}.jpg"), redRegionCropped);

                // 绘制矩形
                Cv2.Rectangle(image, boundingRect, Scalar.Red, 2);
            }



            //Cv2.ImWrite(Path.Combine(_saveFolder, "erodedImage.jpg"), erodedImage);

            //显示图片3
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 3);

            image.Dispose();

            Serilog.Log.Information("识别完成");
        }

        private void Contours()
        {
            // 读取图像文件
            Mat image = Cv2.ImRead(_imgPath, ImreadModes.Color);

            // 如果图像读取失败，打印错误消息并退出
            if (image.Empty())
            {
                Serilog.Log.Warning("无法读取图像文件");
                return;
            }

            //显示原始照片
            ShowImage(OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image), 1);

            // 转换图像为灰度
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);

            // 应用阈值或任何其他预处理以获取二进制图像（如果需要）
            Mat binary = new Mat();
            Cv2.Threshold(gray, binary, 127, 255, ThresholdTypes.Binary);

            // 查找轮廓
            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;
            Cv2.FindContours(binary, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);

            // 遍历轮廓
            foreach (var contour in contours)
            {
                // 获取边界矩形，绘制巨星
                Rect boundingRect = Cv2.BoundingRect(contour);
                Cv2.Rectangle(image, boundingRect, Scalar.Red, 2);

                // 获取最小外接圆
                Cv2.MinEnclosingCircle(contour, out Point2f center, out float radius);
                Cv2.Circle(image, (int)center.X, (int)center.Y, (int)radius, Scalar.Yellow); // 绘制圆
            }

            //显示图片
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
                Title = @"请选择要导入的文件",
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
