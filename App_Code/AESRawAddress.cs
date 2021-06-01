using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for AESRawAddress
/// </summary>
public class AESRawAddress{

    public string diagnostics;   //line 1
    public string line2;         //line 2
    public string address;       //line 3
    public string csz;           //line 4
    public string barcode;       //line 5
    public string city;
    public string state;
    public string zip;
    
    public string error;

	public AESRawAddress(){}
    public AESRawAddress(string line1, string line2, string line3, string line4, string line5){
        diagnostics = line1.Trim();
        this.line2 = line2.Trim();
        address = line3.Trim();
        csz = line4.Trim();
        barcode = line5.Trim();
        error = null;
    }
    public AESRawAddress(string error){
        line2 = address = csz = barcode = diagnostics = null;
        this.error = "AES ERROR:\n'" + error + "'";
    }
    public void parseCSZ(){
        //PARSE City/State/Zip (this does not depend on any other data
        if (csz == null) return;
        csz = csz.Trim();
        int spaceIndex = csz.LastIndexOf(' ');
        if(spaceIndex > 0)
            zip = csz.Substring(spaceIndex + 1,5);
        string cityState = csz.Substring(0, spaceIndex);
        spaceIndex = cityState.LastIndexOf(' ');
        if (spaceIndex > 0){
            state = cityState.Substring(spaceIndex + 1);
            city = cityState.Substring(0, spaceIndex);
        }
    }
}
