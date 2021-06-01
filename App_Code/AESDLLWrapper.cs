using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.InteropServices;
using System.Text;



#region AESDLLWrapper Class

/// <summary>
/// Summary description for AESWrapper
/// </summary>
public class AESDLLWrapper{
    /// <summary>
    /// This is the main interface for AES
    /// </summary>
    /// <param name="RESPONSE">
    ///     ***MUST BE 4098 CHARACTERS***
    ///     StringBuilder RESPONSE = new StringBuilder(4098);
    ///     The buffer is a multiple of five lines CR LF delimited. The lines are defined as:
    ///     Diagnostic: //KEY=%d REL=%d AQF=%e DPV=%d ELOT=%s CNTY=%d
    ///     Línea 1
    ///     Línea 2
    ///     Línea 3
    ///     bar-code
    /// 
    ///     Diagnostic line:
    ///     KEY=%d – integer candidate id sorted by reliability best is KEY=1
    ///     REL=%d – integer reliability can be over 100 (click of CASS window explains)
    ///     AQF=%s – address quality flags described in Mail*STAR document appendix B
    ///     CRT=%s – USPS carrier route
    ///     Note: DPV is included only if DPV is enabled
    ///     DPV=%d – DPV valid bitmap documented in Mail*DPVeLOT appendix B
    ///     Note: ELOT and CNTY are included only if eLOT is enabled
    ///     ELOT=%s – 1 byte {A,D} + 4 byte integer sequence number
    ///     CNTY=%d – county code 
    /// </param>
    /// <param name="REQUEST">
    ///     Input address in this form:
    ///     Address1\n
    ///     Address2\n
    ///     City State Zip
    /// </param>
    /// <returns>long represents number of addresses found in RESPONSE</returns>
    [DllImport(@"msapix.dll")]
    private extern static int AESapigen(StringBuilder RESPONSE, string REQUEST);
    /// <summary>
    /// This sets which AES Server to hit.  It defaults to the values in
    /// the config file so this call is unnecessary for most purposes.
    /// </summary>
    /// <param name="SERVER"></param>
    /// <param name="PORT"></param>
    /// <returns></returns>
    [DllImport(@"msapix.dll")]
    private extern static long AEService(string SERVER, string PORT);

    /// <summary>
    /// Returns an array list of AESRawAddress from the AES using the dll
    /// Returns only maxAddresses number of result addresses in the result
    /// </summary>
    /// <param name="maxAddresses"></param>
    /// <param name="Address"></param>
    /// <param name="Address2"></param>
    /// <param name="City"></param>
    /// <param name="State"></param>
    /// <param name="Zip"></param>
    /// <returns></returns>
    public static AESRawAddress[] GetRawAddresses  (string Address,
                                                    string Address2, 
                                                    string City, 
                                                    string State, 
                                                    string Zip,
                                                    string accountNum,
                                                    string Server,
                                                    string Port)
    {
        //Create result arraylist
        ArrayList rawAddresses = new ArrayList();
        AESRawAddress[] returnMe;

        //Construct the request address
        string aesRequest = "[" + accountNum + "]\n" + Address + "\n" + Address2 + "\n" + City + " " + State + " " + Zip;

        //Response must be a StringBuilder so aes dll can write to buffer
        //Must be length 4098
        StringBuilder aesResponse = new StringBuilder(4098);

        //Configure which server and port to hit, should be done in aes config file, but lets make sure
        AEService(Server, Port);
        int numberResults = AESapigen(aesResponse, aesRequest);

        if(numberResults < 0){
            //ERROR... dump buffer
            returnMe = new AESRawAddress[1];
            returnMe[0] = new AESRawAddress(aesResponse.ToString().Trim());
            return returnMe;
        }

        //Get lines from StringBuilder
        string[] addressLines = aesResponse.ToString().Split('\n');

        for (int i = 0; i < numberResults; i++){
            //Get the 5 lines for each address
            string line1 = addressLines[i * 5].Trim('\r');
            string line2 = addressLines[i * 5 + 1].Trim('\r');
            string line3 = addressLines[i * 5 + 2].Trim('\r');
            string line4 = addressLines[i * 5 + 3].Trim('\r');
            string line5 = addressLines[i * 5 + 4].Trim('\r');

            rawAddresses.Add(new AESRawAddress(line1, line2, line3, line4, line5));

        }//END for each address
        returnMe = new AESRawAddress[rawAddresses.Count];
        for(int i = 0; i < rawAddresses.Count; i++){
            returnMe[i] = (AESRawAddress)rawAddresses[i];
        }
        return returnMe;


    }//END GetRawAddresses

    public static string GetCongressCode(string zip9, string server, string port){
        zip9 = zip9.Replace("-", "");
        zip9 = zip9.PadRight(9, '0');

        //Construct the request buffer
        string aesRequest = "<cmd=" + ConfigurationManager.AppSettings["CongressCodeCommand"]+zip9 + ">";

        //Response must be a StringBuilder so aes dll can write to buffer
        //Must be length 4098
        StringBuilder aesResponse = new StringBuilder(4098);

        //Configure which server and port to hit, should be done in aes config file, but lets make sure
        AEService(server, port);
        AESapigen(aesResponse, aesRequest);

        string result = aesResponse.ToString();
        if (result.StartsWith("<") && result.EndsWith(">")){
            int code = Convert.ToInt32(result.Replace("<", "").Replace(">", ""));
            if (code >= -1)
                return code.ToString().PadLeft(2, '0');
        }

        return "";

    }//End GetCongressCode

}//ENDCLASS AESDLLWrapper

#endregion