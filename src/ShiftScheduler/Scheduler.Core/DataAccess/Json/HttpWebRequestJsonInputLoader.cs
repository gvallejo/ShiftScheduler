using Scheduler.Core.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Core.DataAccess.Json
{
    public class HttpWebRequestJsonInputLoader : IJsonInputLoader
    {

        public HttpWebRequestJsonInputLoader()
        {

        }
        
        public virtual string Load(string path, out int status)
        {

            return HttpGet(path, out status);
        }

        // Returns JSON string
        protected virtual string HttpGet(string url, out int status)
        {
            string resp;
            HttpWebRequest request = null;                       
            HttpWebResponse response = null;
                     
            LogSession.Main.EnterMethod(this, "HttpGet");
            
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();

                status = (int)response.StatusCode;
                //if (response.StatusCode == HttpStatusCode.OK)
                                              
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    resp = reader.ReadToEnd();
                }

                return resp;                
            }
            catch (WebException ex)
            {
                LogSession.Main.LogException(ex);
                WebResponse errorResponse = ex.Response;
                if (errorResponse != null)
                {
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        String errorText = reader.ReadToEnd();
                        LogSession.Main.LogError(errorText);
                    }
                }
                 
                throw ex;
            }
            finally
            {
                // Releases the resources of the response.
                if(response != null)
                    response.Close();
                LogSession.Main.LeaveMethod(this, "HttpGet");
            }
        }

    }
}
