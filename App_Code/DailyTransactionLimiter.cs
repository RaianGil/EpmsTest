using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;

/// <summary>
/// Provides functionality to limit the number of transactions per IP per day.
/// Should be instatiated only once when the server starts... 
/// </summary>
public class DailyTransactionLimiter{

    private int maxHits;
    private Hashtable usageTable;

	public DailyTransactionLimiter(){
        try{
            usageTable = new Hashtable();
            maxHits = (Int32)HttpContext.Current.Application["MaxDailyTransactions"];
        }
        catch(Exception e){
            throw e;
        }
	}

    public bool getPermissionFor(string ip){
        if (!VerifyIP(ip))
            throw new FormatException("Invalid IP Address Format: " + ip.ToString() + ".");
        UpdateUsageTable();
        SetLastUpdateTime(DateTime.Now);
        if(usageTable.ContainsKey(ip)){
            int numHits = (Int32) usageTable[ip];
            if(numHits >= maxHits)return false;
            return true;
        }
        usageTable.Add(ip, (Int32)0);
        return true;
    }

    public int incrementHitsFor(string ip){
         if (!VerifyIP(ip))
            throw new FormatException("Invalid IP Address Format: " + ip.ToString() + ".");
        UpdateUsageTable();
        SetLastUpdateTime(DateTime.Now);
        if(usageTable.ContainsKey(ip)){
            usageTable[ip] = ((Int32) usageTable[ip]) + 1;
            return (Int32)usageTable[ip];
        }
        usageTable.Add(ip, (Int32)1);
        return 1;
    }

    public int getHitsFor(string ip){
         if (!VerifyIP(ip))
            throw new FormatException("Invalid IP Address Format: " + ip.ToString() + ".");
        UpdateUsageTable();
        SetLastUpdateTime(DateTime.Now);
        if(usageTable.ContainsKey(ip)){
            return (Int32)usageTable[ip];
        }
        return 0;
    }


    //Private Helpers
    private bool VerifyIP(string ip){
        Regex ip_test = new Regex("^(\\d{1,3}\\.){3}\\d{1,3}$");
        if(!ip_test.IsMatch(ip)){
            return false;
        }
        return true;
    }

    private void SetLastUpdateTime(DateTime dt){
        HttpContext.Current.Application.Set("LastUpdateTime", dt);
    }

    private void UpdateUsageTable(){
        DateTime lastUpdate = (DateTime) HttpContext.Current.Application["LastUpdateTime"];
        if(DateTime.Now.Year > lastUpdate.Year || DateTime.Now.DayOfYear > lastUpdate.DayOfYear){
            usageTable.Clear();
        }
    }

}
