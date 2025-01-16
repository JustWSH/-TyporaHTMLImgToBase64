using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace TyporaHTMLImgToBase64
{
    internal class Program
    {
        public string inputPath = "";
        readonly string version = "v2";
        static void Main(string[] args)
        {
            //args = new string[] { @"E:\TyporaHTMLImgToBase64\bin\testdata\Host APC Tuning by Wafer2.html" };
            if (args.Length == 0)
            {
                Console.WriteLine("No define input file");
                return;
            }
            Program program = new Program();
            program.StartFunction(args);
        }
        private void StartFunction(string[] args)
        {
            if (args[0]=="-v")
            {
                Console.WriteLine(version);
                return;
            }
            inputPath = args[0];
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"The input file is not exist.\n\r{inputPath}");
                return;
            }
            string inputName = Path.GetFileNameWithoutExtension(inputPath);
            string inputDirectory = Path.GetDirectoryName(inputPath);
            string outputPath = $@"{inputDirectory}\{inputName} - Base64.html";
            ConvertStart(inputPath, outputPath, inputDirectory);
            Console.WriteLine($"Converted. Output to: {outputPath}");
        }
        private void ConvertStart(string inputPath, string outputPath, string inputDirectory)
        {
            // 读取HTML文件内容
            string htmlContent = File.ReadAllText(inputPath);
            // 正则表达式匹配<img>标签中的src属性
            string pattern = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+)[""']?[^>]*>";
            // 使用正则表达式替换匹配到的<img>标签
            htmlContent = Regex.Replace(htmlContent, pattern, new MatchEvaluator(ReplaceImageSrc));
            // 输出新生成的HTML文件路径
            string newHtmlFilePath = outputPath;
            // 将新的HTML内容写入文件
            File.WriteAllText(newHtmlFilePath, htmlContent);
        }
        // 替换<img>标签中的src属性为Base64编码的图片数据
        private string ReplaceImageSrc(Match match)
        {
            // 获取图片文件路径
            string imagePath = match.Groups[1].Value;
            // 获取HTML文件所在的目录路径
            string htmlFileDirectory = Path.GetDirectoryName(inputPath);
            // 获取图片文件的完整路径
            string imageFullPath = Path.Combine(htmlFileDirectory, imagePath);
            // 读取图片文件为字节数组
            byte[] imageBytes = File.ReadAllBytes(imageFullPath);
            // 将字节数组转换为Base64字符串
            string base64String = Convert.ToBase64String(imageBytes);

            // 获取图片文件的扩展名，用于确定MIME类型
            string imageExtension = Path.GetExtension(imagePath).ToLower();
            string mimeType = GetMimeType(imageExtension);

            // 构建新的<img>标签的src属性值
            string newSrc = $"data:{mimeType};base64,{base64String}";

            // 返回替换后的<img>标签
            return match.Value.Replace(imagePath, newSrc);
        }

        // 根据图片文件扩展名获取MIME类型
        static string GetMimeType(string extension)
        {
            switch (extension)
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".bmp":
                    return "image/bmp";
                // 可以根据需要添加更多图片格式的MIME类型
                default:
                    return "application/octet-stream";
            }
        }






    }
}
