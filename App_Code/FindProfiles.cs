using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SearchProfiles
/// </summary>
public class FindProfiles
{
    public FindProfiles()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    public static DataTable getProfiles(string SearchOptions, string SearchBy)
    {
        DataTable profileTable;

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);

        try
        {
            cnn.Open();

            SqlDataAdapter profileDataAdapter = new SqlDataAdapter("SELECT * FROM UserProfileTable LEFT JOIN UserProfileImages ON UserProfileImages.UserName = UserProfileTable.UserName LEFT JOIN aspnet_Users ON aspnet_Users.UserId = UserProfileTable.UserId WHERE UserProfileImages.IsMain = 1" + SearchOptions, cnn);

            profileTable = new System.Data.DataTable();

            DataColumn dateRoundedColumn = new DataColumn("DistanceRounded", typeof(String));
            profileTable.Columns.Add(dateRoundedColumn);

            DataColumn ageColumn = new DataColumn("Age", typeof(Int16));
            profileTable.Columns.Add(ageColumn);

            profileDataAdapter.Fill(profileTable);

            foreach (DataRow r in profileTable.Rows)
            {
                r["Age"] = Int16.Parse(UserProfile.GetAge(r["BirthDate"].ToString()));
                r["DistanceRounded"] = "";
            }

            profileTable = SortDataTable(profileTable, SearchBy);

            DataColumn idColumn = new DataColumn("idColumn", typeof(Int16));
            profileTable.Columns.Add(idColumn);

            int rowNumber = 0;

            foreach (DataRow r in profileTable.Rows)
            {
                r["idColumn"] = rowNumber;
                rowNumber++;
            }
        }
        finally
        {
            cnn.Close();
        }

        return profileTable;
    }

    public static DataTable getProfilesNearZip(string SearchOptions, string SearchBy, string zip, string miles)
    {
        string zips = "";
        DataTable profileTable;

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);
        SqlCommand cmd = new SqlCommand("GetNearZipcode", cnn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = cnn;
        cmd.Parameters.Add(
            new SqlParameter("@Zipcode", zip));
        cmd.Parameters.Add(
            new SqlParameter("@Miles", miles));

        try
        {
            cnn.Open();

            SqlDataAdapter zipsDataAdapter = new SqlDataAdapter(cmd);

            DataTable zipTable = new System.Data.DataTable();
            zipsDataAdapter.Fill(zipTable);

            int i = 0;

            foreach (DataRow row in zipTable.Rows)
            {
                if (i > 0)
                {
                    zips += ",";
                }
                zips += "'" + row["Zipcode"] + "'";
                i++;
            }

            SqlDataAdapter profileDataAdapter = new SqlDataAdapter("SELECT * FROM UserProfileTable LEFT JOIN UserProfileImages ON UserProfileImages.UserName = UserProfileTable.UserName LEFT JOIN aspnet_Users ON aspnet_Users.UserId = UserProfileTable.UserId WHERE UserProfileTable.Zip IN (" + zips + ") AND UserProfileImages.IsMain = 1" + SearchOptions, cnn);

            profileTable = new System.Data.DataTable();

            //DataColumn dc = new DataColumn("Distance", System.Type.GetType("System.String"));
            DataColumn dateColumn = new DataColumn("Distance", typeof(Double));
            profileTable.Columns.Add(dateColumn);

            DataColumn dateRoundedColumn = new DataColumn("DistanceRounded", typeof(String));
            profileTable.Columns.Add(dateRoundedColumn);

            DataColumn ageColumn = new DataColumn("Age", typeof(Int16));
            profileTable.Columns.Add(ageColumn);

            profileDataAdapter.Fill(profileTable);

            foreach (DataRow r in profileTable.Rows)
            {
                r["Distance"] = Double.Parse(getDistanceByZip(zipTable, r["Zip"].ToString()));
                r["DistanceRounded"] = "Within " + (((int)Math.Ceiling(Double.Parse(r["Distance"].ToString()) / 5)) * 5).ToString() + " miles.";
                r["Age"] = Int16.Parse(UserProfile.GetAge(r["BirthDate"].ToString()));
            }

            //profileTable.DefaultView.Sort = "Distance ASC";
            //profileTable = SortDataTable(profileTable, "Distance ASC");
            profileTable = SortDataTable(profileTable, SearchBy);

            DataColumn idColumn = new DataColumn("idColumn", typeof(Int16));
            profileTable.Columns.Add(idColumn);

            int rowNumber = 0;

            foreach (DataRow r in profileTable.Rows)
            {
                r["idColumn"] = rowNumber;
                rowNumber++;
            }
        }
        finally
        {
            cnn.Close();
        }

        return profileTable;
    }

    public static DataTable getLastActive(int limit)
    {
        DataTable profileTable;

        ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["MainDatabase"];
        SqlConnection cnn = new SqlConnection(connectionStringSettings.ConnectionString);

        try
        {
            cnn.Open();

            SqlDataAdapter profileDataAdapter = new SqlDataAdapter("SELECT TOP " + limit.ToString() + " * FROM UserProfileTable LEFT JOIN UserProfileImages ON UserProfileImages.UserName = UserProfileTable.UserName LEFT JOIN aspnet_Users ON aspnet_Users.UserId = UserProfileTable.UserId WHERE UserProfileImages.IsMain = 1 ORDER BY aspnet_Users.LastActivityDate DESC", cnn);

            profileTable = new System.Data.DataTable();

            DataColumn dateRoundedColumn = new DataColumn("DistanceRounded", typeof(String));
            profileTable.Columns.Add(dateRoundedColumn);

            DataColumn ageColumn = new DataColumn("Age", typeof(Int16));
            profileTable.Columns.Add(ageColumn);

            profileDataAdapter.Fill(profileTable);

            foreach (DataRow r in profileTable.Rows)
            {
                r["Age"] = Int16.Parse(UserProfile.GetAge(r["BirthDate"].ToString()));
                r["DistanceRounded"] = "";
            }

            profileTable = SortDataTable(profileTable, "LastActivityDate DESC");

            DataColumn idColumn = new DataColumn("idColumn", typeof(Int16));
            profileTable.Columns.Add(idColumn);

            int rowNumber = 0;

            foreach (DataRow r in profileTable.Rows)
            {
                r["idColumn"] = rowNumber;
                rowNumber++;
            }
        }
        finally
        {
            cnn.Close();
        }

        return profileTable;
    }

    public static string getDistanceByZip(DataTable zipTable, string zip)
    {
        string distance = "";

        foreach (DataRow row in zipTable.Rows)
        {
            if (row["Zipcode"].ToString() == zip)
            {
                distance = row["Distance"].ToString();
                break;
            }
        }

        return distance;
    }

    public static DataTable SortDataTable(DataTable GetDataTable, string sort)
    {
        DataTable _NewDataTable = GetDataTable.Clone();
        int rowCount = GetDataTable.Rows.Count;

        DataRow[] foundRows = GetDataTable.Select(null, sort);

        // Sort with Column name
        for (int i = 0; i < rowCount; i++)
        {
            object[] arr = new object[GetDataTable.Columns.Count];

            for (int j = 0; j < GetDataTable.Columns.Count; j++)
            {
                arr[j] = foundRows[i][j];
            }

            DataRow data_row = _NewDataTable.NewRow();
            data_row.ItemArray = arr;
            _NewDataTable.Rows.Add(data_row);
        }

        //Clear the incoming GetDataTable
        GetDataTable.Rows.Clear();

        for (int i = 0; i < _NewDataTable.Rows.Count; i++)
        {
            object[] arr = new object[GetDataTable.Columns.Count];

            for (int j = 0; j < GetDataTable.Columns.Count; j++)
            {
                arr[j] = _NewDataTable.Rows[i][j];
            }

            DataRow data_row = GetDataTable.NewRow();

            data_row.ItemArray = arr;

            GetDataTable.Rows.Add(data_row);
        }

        return _NewDataTable;
    }
}
