using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable recentlyActiveTable = FindProfiles.getLastActive(5);
        SearchRepeater.DataSource = recentlyActiveTable.DefaultView;
        SearchRepeater.DataBind();
    }

    public string truncateString(string inputString, int maxLength)
    {
        if (inputString.Length > maxLength)
        {
            if (maxLength > 40)
            {
                return inputString.Substring(0, maxLength - 4) + " ...";
            }
            else
            {
                return inputString.Substring(0, maxLength);
            }
        }
        else
        {
            return inputString;
        }
    }
}
