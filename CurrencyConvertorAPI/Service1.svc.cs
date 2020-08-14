using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace CurrencyConvertorAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public Result GetAmountInWords(string value)
        {
           
            Calculation calcObj = new Calculation();
            string pattern = @"^(\d{0,9})(,\d{0,2})?$";
            RegexOptions options = RegexOptions.Singleline;
            Regex regex = new Regex(pattern, options);
            double inputValueNum;

            if (!Double.TryParse(value.Replace(" ", "").ToString(), out inputValueNum))
            {
                Result resObj = new Result();
                resObj.amountInWord = "";
                resObj.errrorMessage = "Currency is not in valid format.";
                return resObj;
            }

            value = Convert.ToDouble(value.Replace(" ", "")).ToString();
            Match match = regex.Match(value);
           
            if (!match.Success)
            {
                Result resObj = new Result();
                resObj.amountInWord = "";
                resObj.errrorMessage = "Currency is not in valid format.";
                return resObj;
            }
           
            return calcObj.ProcessCurrencyConversion(value);
        }

        public string TestWebservice()
        {
            return "Webservice is running";
        }
    }

    public class Result
    {
        [DataMember]
        public string amountInWord = string.Empty;
        [DataMember]
        public string errrorMessage = string.Empty;
    }
}
