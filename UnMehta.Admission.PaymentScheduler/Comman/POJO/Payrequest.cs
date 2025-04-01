using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payrequest
{
    public class HeadDetails
    {
        public string version { get; set; }
        public string api { get; set; }
        public string platform { get; set; }

    }

    public class HeadDetails1
    {
        public string api { get; set; }
        public string source { get; set; }

    }

    public class MerchDetails1
    {
        public string merchId { get; set; }
        public string password { get; set; }
        public string merchTxnId { get; set; }
        public string merchTxnDate { get; set; }

    }
    public class PayDetails1
    {
        public string amount { get; set; }
        public string txnCurrency { get; set; }
        public string signature { get; set; }

    }

    public class MerchDetails
    {

        public string merchId { get; set; }
        public string userId { get; set; }
        public string password { get; set; }
        public string merchTxnDate { get; set; }
        public string merchTxnId { get; set; }


    }
    public class PayDetails
    {
        public string amount { get; set; }
        public string product { get; set; }
        public string custAccNo { get; set; }
        public string txnCurrency { get; set; }




    }
    public class CustDetails
    {
        public string custEmail { get; set; }
        public string custMobile { get; set; }


    }
    public class Extras
    {
        public string udf1 { get; set; }
        public string udf2 { get; set; }
        public string udf3 { get; set; }
        public string udf4 { get; set; }
        public string udf5 { get; set; }


    }

    public class MsgBdy
    {
        public HeadDetails headDetails { get; set; }
        public MerchDetails merchDetails { get; set; }
        public PayDetails payDetails { get; set; }
        public CustDetails custDetails { get; set; }
        public Extras extras { get; set; }




    }
    public class Payrequest
    {
        public HeadDetails headDetails { get; set; }
        // public MsgBdy msgBdy { get; set; }
        public MerchDetails merchDetails { get; set; }
        public PayDetails payDetails { get; set; }
        public CustDetails custDetails { get; set; }
        public Extras extras { get; set; }
    }

    public class Statusrequest
    {
        public HeadDetails1 headDetails { get; set; }
        public MerchDetails1 merchDetails { get; set; }
        public PayDetails1 payDetails { get; set; }

    }

    public class RootObject
    {
        public Payrequest payInstrument { get; set; }
    }

    public class RootObjectStatus
    {
        public Statusrequest payInstrument { get; set; }
    }
}