using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyConvertorAPI
{
    public class Calculation
    {
        /// <summary>
        /// Entry point for the Algorithm, 
        /// </summary>
        /// <param name="inputNumber">Currency in Numbers</param>
        /// <returns></returns>
        public Result ProcessCurrencyConversion(String inputNumber)
        {
            Result resultObj;
            String result = "", wholeNumber = inputNumber, decimalPoint = String.Empty, andIdentifier = String.Empty, decimalPointInWord = String.Empty, centIdentifier = String.Empty;
            try
            {
                resultObj = new Result();
                int decimalPlace = inputNumber.IndexOf(",");
                
                if (decimalPlace > 0) 
                {
                    // Execute this condition only for cents
                    wholeNumber = inputNumber.Substring(0, decimalPlace);
                    decimalPoint = inputNumber.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(decimalPoint) > 0)
                    {
                        decimalPoint = decimalPoint.Length == 1 ? decimalPoint + "0" : decimalPoint;
                        andIdentifier = "and";
                        centIdentifier = Convert.ToInt16(decimalPoint) > 1 ? "cents " : "cent " + centIdentifier;
                        decimalPointInWord = DecimalToWordConversion(decimalPoint);
                    }
                }
                result = String.Format("{0} {1} {2} {3} ", Convert.ToUInt64(wholeNumber) > 1 ? ConvertWholeNumber(wholeNumber).Trim() + " dollars" : (Convert.ToUInt64(wholeNumber) == 0 ? "zero dollars" : ConvertWholeNumber(wholeNumber).Trim() + " dollar"), andIdentifier, decimalPointInWord, centIdentifier).ToLower();
                resultObj.amountInWord = result.Trim();
                resultObj.errrorMessage = String.Empty;
                return resultObj;
            }
            catch (Exception ex)
            {
                resultObj = new Result();
                resultObj.amountInWord = String.Empty;
                resultObj.errrorMessage = ex.Message;
                return resultObj;
            }
        }

        /// <summary>
        /// This method is used to perform Cent conversion into words
        /// </summary>
        /// <param name="decimalFraction">Cent in Numbers</param>
        /// <returns></returns>
        public String DecimalToWordConversion(String decimalFraction)
        {
            String centInWord = String.Empty;
            int decimalAmount = Convert.ToInt16(decimalFraction);

            if (decimalAmount == 0)
                centInWord = "Zero";
            else if (decimalFraction.StartsWith("0"))
                centInWord = GetOnes(decimalAmount.ToString());
            else
                centInWord = GetTens(decimalAmount.ToString());

            return centInWord;
        }

        /// <summary>
        ///  This method is used to perform Dollar conversion into words, this function call recursively untill it break whole amount into Tens and Ones.
        /// </summary>
        /// <param name="wholeNumber">Dollar in Numbers</param>
        /// <returns></returns>
        public String ConvertWholeNumber(String wholeNumber)
        {
            string result = String.Empty;
            bool isConversionCompleted = false; 
            double wholeNumberAmount = (Convert.ToDouble(wholeNumber));

            if (wholeNumberAmount > 0)
            {
                int wholeNumberAmountLength = wholeNumber.Length, wholeNumberAmountPlace = 0;
                String wholeNumberAmountPlaceStr = String.Empty;
                switch (wholeNumberAmountLength)
                {
                    case 1://Range for Ones
                        result = GetOnes(wholeNumber);
                        isConversionCompleted = true;
                        break;
                    case 2://Range for Tens
                        result = GetTens(wholeNumber);
                        isConversionCompleted = true;
                        break;
                    case 3://Range for Hundreds
                        wholeNumberAmountPlace = (wholeNumberAmountLength % 3) + 1;
                        wholeNumberAmountPlaceStr = " Hundred ";
                        break;
                    case 4://Range for Thousands    
                    case 5://Range for Thousands   
                    case 6://Range for Thousands   
                        wholeNumberAmountPlace = (wholeNumberAmountLength % 4) + 1;
                        wholeNumberAmountPlaceStr = " Thousand ";
                        break;
                    case 7://Range for Millions 
                    case 8://Range for Millions 
                    case 9://Range for Millions 
                        wholeNumberAmountPlace = (wholeNumberAmountLength % 7) + 1;
                        wholeNumberAmountPlaceStr = " Million ";
                        break;
                    case 10://Range for Billions
                    case 11://Range for Billions
                    case 12://Range for Billions
                        wholeNumberAmountPlace = (wholeNumberAmountLength % 10) + 1;
                        wholeNumberAmountPlaceStr = " Billion ";
                        break;
                    default:
                        isConversionCompleted = true;
                        break;
                }
                if (!isConversionCompleted)
                {
                    if (wholeNumber.Substring(0, wholeNumberAmountPlace) != "0" && wholeNumber.Substring(wholeNumberAmountPlace) != "0")
                    {
                        try
                        {
                            result = ConvertWholeNumber(wholeNumber.Substring(0, wholeNumberAmountPlace)) + wholeNumberAmountPlaceStr + ConvertWholeNumber(wholeNumber.Substring(wholeNumberAmountPlace));
                        }
                        catch { }
                    }
                    else
                    {
                        result = ConvertWholeNumber(wholeNumber.Substring(0, wholeNumberAmountPlace)) + ConvertWholeNumber(wholeNumber.Substring(wholeNumberAmountPlace));
                    }

                }
            }
            return result.Trim();
        }

        /// <summary>
        /// To get Ones in words
        /// </summary>
        /// <param name="Number">Single Number</param>
        /// <returns></returns>
        public String GetOnes(String Number)
        {
            int inputOne = Convert.ToInt32(Number);
            Dictionary<int, string> onesCollection = new Dictionary<int, string>();

            onesCollection.Add(1, "One");
            onesCollection.Add(2, "Two");
            onesCollection.Add(3, "Three");
            onesCollection.Add(4, "Four");
            onesCollection.Add(5, "Five");
            onesCollection.Add(6, "Six");
            onesCollection.Add(7, "Seven");
            onesCollection.Add(8, "Eight");
            onesCollection.Add(9, "Nine");

            return onesCollection.ContainsKey(inputOne) ? onesCollection[inputOne] : string.Empty;
        }

        /// <summary>
        ///  To get Tens in words
        /// </summary>
        /// <param name="Number">two digit number</param>
        /// <returns></returns>
        public String GetTens(String Number)
        {
            int inputTen = Convert.ToInt32(Number);
            String tenInWord = String.Empty;

            Dictionary<int, string> tensCollection = new Dictionary<int, string>();

            tensCollection.Add(10, "Ten");
            tensCollection.Add(11, "Eleven");
            tensCollection.Add(12, "Twelve");
            tensCollection.Add(13, "Thirteen");
            tensCollection.Add(14, "Fourteen");
            tensCollection.Add(15, "Fifteen");
            tensCollection.Add(16, "Sixteen");
            tensCollection.Add(17, "Seventeen");
            tensCollection.Add(18, "Eighteen");
            tensCollection.Add(19, "Nineteen");
            tensCollection.Add(20, "Twenty");
            tensCollection.Add(30, "Thirty");
            tensCollection.Add(40, "Fourty");
            tensCollection.Add(50, "Fifty");
            tensCollection.Add(60, "Sixty");
            tensCollection.Add(70, "Seventy");
            tensCollection.Add(80, "Eighty");
            tensCollection.Add(90, "Ninety");

            if (!tensCollection.ContainsKey(inputTen))
            {
                if (inputTen > 0)
                {
                    tenInWord = GetTens(Number.Substring(0, 1) + "0") + "-" + GetOnes(Number.Substring(1));
                }
            }
            else
            {
                tenInWord = tensCollection[inputTen];
            }

            return tenInWord;
        }

    }
}