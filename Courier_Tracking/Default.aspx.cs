using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using System.Data;
using System.Xml.Serialization;
using System.Net;
using System.IO;

namespace Courier_Tracking
{
     
    public partial class _Default : Page
    {
        string aa5, dat, tim,aa6, aa4, fdat="0";
        AwsFilOb Aaa2;
        string tblstring = "";
        string tablestring = "";

        protected void Page_Load(object sender, EventArgs e)
        {
 
        }
        protected void sub_btn_Click(object sender, EventArgs e)
        {
            string trackID = track_id.Text.ToString();
            read();

        }
  
        public void read()
        {
            try
            {
               
            //dt is datatable object which holds DB results.
            string tbl = "";
             String path = "http://plapi.ecomexpress.in/track_me/api/mawbd/?awb=" + track_id.Text +"&order=&username=kkrowtenindia352080_ent&password=KYPAWnPuu4Mf97TT";
            //String path = "http://plapi.ecomexpress.in/track_me/api/mawbd/?awb=294345961&order=&username=kkrowtenindia352080_ent&password=KYPAWnPuu4Mf97TT";
            XmlSerializer ser = new XmlSerializer(typeof(aws));
            WebClient client = new WebClient();
            string data = Encoding.Default.GetString(client.DownloadData(path));
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            aws awso = (aws)ser.Deserialize(stream);
            if (awso.@object != null)
            {
                var one = awso.version;
                var two = awso.@object;
                FileObject Fobj = awso.@object;
                if (Fobj.field != null)
                {
                    var coun = Fobj.field.Count();
                    for (int i = 0; i < coun; i++)
                    {
                        AwsObject A1 = Fobj.field[i];
                        if (A1.@object != null)
                        {
                            var cou = A1.@object.Count();
                                //for (int ii = 0; ii < cou; ii++)
                                //{
                                for (int ii = 0; ii < 1; ii++)
                                {
                                    XmObject aa1 = A1.@object[ii];
                                    if (aa1.field != null)
                                    {
                                        var co = aa1.field.Count();

                                        DataTable dtt = new DataTable();

                                  // Tracking Status Checking Image code start
                                        
                                    for (int ij = 0; ij < 2; ij++)
                                    {
                                        AwsFilOb Aa2 = aa1.field[ij];
                                        string a4 = Aa2.name;
                                        string a5 = Aa2.Value;

                                            if (a5 == "Shipment delivered")
                                            {
                                                tablestring = "<div  class='order-status-timeline'><div class='order-status-timeline-completion c4'></div></div>";
                                            }
                                            else if (a5 == "In-Transit" || a5 == "Out for Pickup" || a5 == "Pickup Assigned" || a5 == "Out for Pickup" || a5 == "Bag scanned at DC" || a5 == "Bag connected from HUB" || a5 == "Bag scanned at Hub" || a5 == "Bag scanned at Hub" || a5 == "Bag connected from HUB" || a5 == "Shipment Picked Up")
                                            {

                                                tablestring = "<div  class='order-status-timeline'><div class='order-status-timeline-completion c2'></div></div>";
                                                //sts_lbl.Text = a5;
                                            }
                                            else if (a5 == "Soft data uploaded")
                                            {
                                                tablestring = "<div  class='order-status-timeline'><div class='order-status-timeline-completion c0'></div></div>";
                                            }
                                            else
                                            {
                                                tablestring = "<div  class='order-status-timeline'><div class='order-status-timeline-completion c0'></div></div>";
                                            }
                                            if (a4 == "updated_on")
                                            {
                                             //   Loc_lbl.Text = a5;
                                               
                                            }
                                        }

                                }

                                tbl = "<div class='image-order-status image-order-status-new active img-circle'> <span class='status'>In Process</span> <div class='icon'></div></div>    <div class='image-order-status image-order-status-active active img-circle'><span class='status'>Item Shipped</span><div class='icon'></div></div>            <div class='image-order-status image-order-status-intransit active img-circle'>  <span class='status'>Item in Transit</span><div class='icon'></div> </div>        <div class='image-order-status image-order-status-delivered active img-circle'><span class='status'>Delivered</span>  <div class='icon'></div> </div>      <div class='image-order-status image-order-status-completed active img-circle'><span class='status'>Completed</span><div class='icon'></div></div>";
                                   
                                divTable.InnerHtml = tablestring + tbl;
                               
                            }

                                // Tracking Status Checking Image code End

                                //-----------------------------------------

                                // Date and time wise status checking code start

                                ViewState["date"] = "0";
                                tblstring = tblstring + "<table width='100%' border='1'>";
                                for (int ii = 0; ii < cou; ii++)
                                {

                                    XmObject aa1 = A1.@object[ii];
                                    if (aa1.field != null)
                                    {
                                        var co = aa1.field.Count();

                                        DataTable dtt = new DataTable();
                                       
                                        for (int iij = 0; iij < co; iij++)
                                        {
                                            Aaa2 = aa1.field[iij];
                                             aa4 = Aaa2.name;

                                            if (aa4 == "updated_on")
                                            {
                                                aa5 = Aaa2.Value;
                                                dat = aa5.Substring(0, 12);
                                                if (ViewState["date"].ToString() == dat)
                                                {
                                                    tim = aa5.Substring(13, 6);
                                                }
                                                else
                                                {
                                                    ViewState["date"] = dat;
                                                    if (fdat == "0")
                                                    {
                                                        dat_lbl.Text = dat;
                                                        fdat = "1";
                                                    }
                                                    tim = aa5.Substring(13, 6);
                                                    tblstring = tblstring + "<tr><td><b>" + dat + "</b></td><td> </td></tr>";
                                                }                                          
                                           }
                                            else if (aa4 == "status")
                                            {
                                                aa6 = Aaa2.Value;
                                               
                                            }
                                            
                                        }
                                        tblstring = tblstring + "<tr><td>" + tim + "</td><td>" + aa6 + "</td></tr>";
                                        ViewState["date"] = dat;
                                    }
                                }
                                tblstring = tblstring + "</table>";
                                divTable1.InnerHtml = tblstring;
                            }

                            // Date and time wise status checking code End

                            else
                            {
                            string a2 = A1.name;
                            string a3 = A1.Text;
                               
                                if (a2== "orderid")
                                {
                                    oid.Text = track_id.Text;
                                    onum.Text = a3;
                                }
                                if (a2 == "tracking_status")
                                {
                                      sts.Text = a3;
                                }
                                if (a2 == "city")
                                {
                              //  Loc_lbl.Text = a3;
                                }
                        }
                    }
                }
            }
                else
                {
                    divTable.InnerHtml = "";
                    divTable1.InnerHtml = "";
                    oid.Text = "";
                    onum.Text = "";
                    sts.Text = "";
                    dat_lbl.Text = "";
                    track_id.Text = "";

                      Response.Write("<script>alert('AWB Number Not Valid');</script>");
                     
                }
            }
            catch (Exception ex)
            {
                divTable.InnerHtml = "";
                divTable1.InnerHtml = "";
                oid.Text = "";
                onum.Text = "";
                sts.Text = "";
                dat_lbl.Text = "";
                track_id.Text = "";
                Response.Write("<script>alert('AWB Number Not Valid');</script>");
            }

        }
    }
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute("ecomexpress-objects", Namespace = "", IsNullable = false)]
    public partial class aws
    {
        public FileObject @object { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FileObject
    {
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public AwsObject[] field { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte pk { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string model { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AwsObject
    {
        [System.Xml.Serialization.XmlElementAttribute("object")]
        public XmObject[] @object { get; set; }

        [System.Xml.Serialization.XmlTextAttribute()]
        public string Text { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get; set; }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XmObject
    {
        [System.Xml.Serialization.XmlElementAttribute("field")]
        public AwsFilOb[] field { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte pk { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string model { get; set; }

    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class AwsFilOb
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type { get; set; }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name { get; set; }


        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value { get; set; }

    }
}







