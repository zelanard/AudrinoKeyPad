using System;
using System.IO;
using System.Net;
using System.Text;

namespace AudrinoKeyPad
{
    internal class WebServer
    {
        /// <summary>
        /// Initiate the webserver.
        /// </summary>
        public void Host()
        {
            string url = "http://localhost:8080/"; // Change the URL and port as needed
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine($"Listening for requests on {url}");

            Run(listener);
        }

        /// <summary>
        /// Run the web server.
        /// </summary>
        /// <param name="listener"></param>
        private static void Run(HttpListener listener)
        {
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerResponse response = context.Response;
                HttpListenerRequest request = context.Request;

                string basePath = @"D:\Skole\Source\Opgaver\AudrinoKeyPad\AudrinoKeyPad"; //the base path of the website.
                string requestUrl = basePath + request.Url.LocalPath; //the base path with the requested path.

                switch (requestUrl)
                {
                    case string html when html.EndsWith(@"D:\Skole\Source\Opgaver\AudrinoKeyPad\AudrinoKeyPad/"):
                        // If the request is for the root path, serve "index.html"
                        ServeFile(response, @"D:\Skole\Source\Opgaver\AudrinoKeyPad\AudrinoKeyPad\index.html", "html");
                        break;
                    case string html when html.EndsWith(".html"):
                        // Serve other HTML files
                        ServeFile(response, requestUrl, "html");
                        break;
                    case string html when html.EndsWith(".css"):
                        // Serve CSS files
                        ServeFile(response, requestUrl, "css");
                        break;
                    case string html when html.EndsWith(".js"):
                        // Serve JavaScrit files
                        ServeFile(response, requestUrl, "js");
                        break;
                    default:
                        // Handle 404 Not Found for other file types
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                }

                response.Close();
            }
        }

        /// <summary>
        /// Serve the file to the browser.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="fileName"></param>
        /// <param name="fileType"></param>
        static void ServeFile(HttpListenerResponse response, string fileName, string fileType)
        {
            // Set the response content type to JavaScript with UTF-8 charset
            switch (fileType)
            {
                case "html":
                    response.ContentType = "text/html; charset=utf-8";
                    break;
                case "css":
                    response.ContentType = "text/css";
                    break;
                case "js":
                    response.ContentType = "application/javascript; charset=utf-8";
                    break;
            }

            // Construct the path to the file
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

            if (File.Exists(filePath))
            {
                // Read the file content
                string content = File.ReadAllText(filePath);

                // Convert the string to bytes using UTF-8 encoding
                byte[] buffer = Encoding.UTF8.GetBytes(content);

                // write the content to the response stream 
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                // Handle 404 Not Found for missing files
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }

        /// <summary>
        /// File types
        /// </summary>
        enum FileType
        {
            html,
            css,
            js,
            txt
        }
    }
}
