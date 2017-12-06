﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WholesomeMVC.WebForms
{
    public partial class indexresult : System.Web.UI.Page
    {
        // Counter to keep up with save id's
        public static String savedNdb_no = "";
        public static String savedItemName = "";
        public static double savedNrf6 = 0;
        public static String savedFoodGroup = "";

        public static string number;
        public static string ing;
        public static int counter = 0;
        public static DataTable dataSearchResults = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // do nothing
            }
            else
            {
                // set page variables
                String strTitle = "Search Results";

                Literal page_title = (Literal)Master.FindControl("page_title");
                page_title.Text = strTitle;
                Label body_title = (Label)Master.FindControl("body_title");
                body_title.Text = strTitle;

                image_grain.ImageUrl = "/Content/Images/icons8-wheat-100.png";
                image_vegetables.ImageUrl = "/Content/Images/icons8-broccoli-100.png";
                image_fruit.ImageUrl = "/Content/Images/icons8-apple-100.png";
                image_dairy.ImageUrl = "/Content/Images/icons8-cheese-100.png";
                image_baby_food.ImageUrl = "/Content/Images/icons8-baby-bottle-100.png";
                image_beverages.ImageUrl = "/Content/Images/icons8-wine-glass-100.png";

                button_grain.Text = "Grain";
                button_vegetables.Text = "Vegetables";
                button_fruit.Text = "Fruit";
                button_dairy.Text = "Dairy";
                button_baby_food.Text = "Baby Food";
                button_beverages.Text = "Beverages";

                BindDataFromDB();
            }
        }

        /***
		 * Use Pull_New_Ceres_Items and Update_Ceres_Items stored procedure to fetch data.
		 * Loop through the data set.
		 * For each row in the data set, call GenerateHtmlForEachItem() to generate HTML code.
		 * Render generated HTML to search_results section.
		 */
        protected void BindDataFromDB()
        {
            System.Data.SqlClient.SqlConnection sc = new System.Data.SqlClient.SqlConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
            };

            sc.Open();

            SqlCommand myCommand = new SqlCommand("Pull_New_Ceres_Items", sc);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.ExecuteNonQuery();

            myCommand = new SqlCommand("Update_Ceres_Items", sc);
            myCommand.ExecuteNonQuery();

            //myCommand = new SqlCommand("Update_Wholesome_Items", sc);
            //myCommand.ExecuteNonQuery();

            sc.Close();

            search_summary.Text = String.Format("Found {0} items", dataSearchResults.Rows.Count);
            filter_applied.Text = String.Format("Filter applied: {0}", "none");

            String strHTML = "";
            foreach (DataRow row in dataSearchResults.Rows)
            {
                strHTML += GenerateHtmlForEachItem(row);
            }

            search_results.InnerHtml = strHTML;
        }

        /***
		 * Take a data row (aka item) of the result set as argument.
		 * Generate a string contains a snippet of HTML code and the item's name and score.
		 * Style the foreground color of the score and the background color of the expand button.
		 * Return generated string.
		 */
        protected String GenerateHtmlForEachItem(DataRow item)
        {
            String returnValue = "";
            String colorScaleStyle = "";
            double score = double.Parse(item["ND Score"].ToString());

            if (score < 0)
            {
                colorScaleStyle = GradientColors.getColor1();
            }
            else if ((score >= 0) && (score <= 2.33))
            {
                colorScaleStyle = GradientColors.getColor2();
            }
            else if ((score > 2.33) && (score <= 4.66))
            {
                colorScaleStyle = GradientColors.getColor3();
            }
            else if ((score > 4.66) && (score <= 12.44))
            {
                colorScaleStyle = GradientColors.getColor4();
            }
            else if ((score > 12.44) && (score <= 20.22))
            {
                colorScaleStyle = GradientColors.getColor5();
            }
            else if ((score > 20.22) && (score <= 28))
            {
                colorScaleStyle = GradientColors.getColor6();
            }
            else if ((score > 28) && (score <= 35.33))
            {
                colorScaleStyle = GradientColors.getColor7();
            }
            else if ((score > 35.33) && (score <= 42.67))
            {
                colorScaleStyle = GradientColors.getColor8();
            }
            else if (score > 42.67)
            {
                colorScaleStyle = GradientColors.getColor9();
            }
            else
            {
                // do nothing
            }

            colorScaleStyle += " !important";

            returnValue = String.Format(@"
				<div class='col-sm-6 col-md-4 col-lg-3'>
					<div class='panel panel-default' style='border-bottom: 5px solid {0};'>
						<div class='panel-body'>
							<h4 id='{3}_name' class='panel-title equal-height'>{1}</h4>
							<h4><strong>ND_Score: <span style='color: {0};'>{2}<span></strong></h4>
							<button id='{3}' class='btn btn-default btn-block expend-button' data-toggle='modal' data-target='#expanded_view'>Expand</button>
						</div>
					</div>
				</div>
			",
            colorScaleStyle,
            item["name"].ToString(),
            item["ND score"].ToString(),
            item["NDBno"].ToString());

            return returnValue;
        }

        /***
		 * Get the ndbno from a hidden field in front-end.
		 * Get data using FoodItem.findNdbno method and ndbno.
		 * Style the NR Score accordingly.
		 * Update data to modal section in front-end.
		 */
        protected void ExpandItem(object sender, EventArgs e)
        {
            string ndbno = lblNdbno.Value;
            FoodItem.findNdbno(ndbno);

            double score = FoodItem.newFood.NRF6;
            String colorScaleStyle = "";

            if (score < 0)
            {
                colorScaleStyle = GradientColors.getColor1();
            }
            else if ((score >= 0) && (score <= 2.33))
            {
                colorScaleStyle = GradientColors.getColor2();
            }
            else if ((score > 2.33) && (score <= 4.66))
            {
                colorScaleStyle = GradientColors.getColor3();
            }
            else if ((score > 4.66) && (score <= 12.44))
            {
                colorScaleStyle = GradientColors.getColor4();
            }
            else if ((score > 12.44) && (score <= 20.22))
            {
                colorScaleStyle = GradientColors.getColor5();
            }
            else if ((score > 20.22) && (score <= 28))
            {
                colorScaleStyle = GradientColors.getColor6();
            }
            else if ((score > 28) && (score <= 35.33))
            {
                colorScaleStyle = GradientColors.getColor7();
            }
            else if ((score > 35.33) && (score <= 42.67))
            {
                colorScaleStyle = GradientColors.getColor8();
            }
            else if (score > 42.67)
            {
                colorScaleStyle = GradientColors.getColor9();
            }
            else
            {
                // do nothing
            }

            lblIndexResult.ForeColor = ColorTranslator.FromHtml(colorScaleStyle);
            modal_header.Attributes["style"] = String.Format("border-bottom: 5px solid {0};", colorScaleStyle);

            lblFoodName.Text = FoodItem.newFood.name;
            lblIndexResult.Text = Convert.ToString(Math.Round(score, 2));
            lblNdbno.Value = FoodItem.newFood.ndbNo;
            lblName.Value = FoodItem.newFood.name;

            txtcalories.Text = FoodItem.newFood.kCal.ToString();
            txtsatfat.Text = Math.Round(FoodItem.newFood.satFat, 2).ToString();
            txtsodium.Text = Math.Round(FoodItem.newFood.sodium, 2).ToString();
            txtfiber.Text = Math.Round(FoodItem.newFood.fiber, 2).ToString();
            txtsugar.Text = Math.Round(FoodItem.newFood.totalSugar, 2).ToString();
            txtprotein.Text = Math.Round(FoodItem.newFood.protein, 2).ToString();
            txtva.Text = Math.Round((FoodItem.newFood.vitaminA / 5000) * 100).ToString();
            txtvc.Text = Math.Round((FoodItem.newFood.vitaminC / 60) * 100).ToString();
            txtcalcium.Text = Math.Round((FoodItem.newFood.calcium / 1000) * 100).ToString();
            txtiron.Text = Math.Round((FoodItem.newFood.iron / 18) * 100).ToString();
        }

        /***
		 * Using the static saved data in this class, update Wholesome_Item table.
		 * 
		 * TODO: 
		 * Check if the ceres item is null.
		 * If ceres item doesn't exist prompt the user to open ceres and enter it there first.
		 */
        protected void CompareItem(object sender, EventArgs e)
        {

            bool flag = false;
            String ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            String ndbno = lblNdbno.Value;


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("Select ndb_no From dbo.Comparison_Item WHERE ndb_no = @ndb_no", connection);
                command.Parameters.Add("@ndb_no", SqlDbType.NVarChar, 8).Value = ndbno;
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (reader[0].ToString() == ndbno)
                    {
                        flag = true;
                        break;
                    }

                }
                if (flag)
                {
                    // food already been compared
                }
                else
                    connection.Close();
                    connection.Open();
                {
                    SqlCommand command1 = new SqlCommand();
                    command1.Connection = connection;
                    command1.CommandType = System.Data.CommandType.Text;

                    command1.CommandText = @"
					INSERT INTO [wholesomeDB].[dbo].[Comparison_Item] (
						[ndb_no],
						[nrf6],
						[FoodName],
						[loginID],
						[protein],
						[fiber],
						[VitaminA],
						[VitaminC],
						[Calcium],
						[Iron],
						[SaturatedFat],
						[TotalSugar],
						[Sodium],
						[KCal],
                        [LastupdatedBy],
                        [LastUpdated]
					) VALUES (
						@ndbno,
						@nrf6,
						@name,
						@loginid,
						@protein,
						@fiber,
						@va,
						@vc,
						@calcium,
						@iron,
						@satfat,
						@sugar,
						@sodium,
						@calories,
                        @lastupdatedby,
                        @lastupdated
					)
				";
                    command1.Parameters.Add("@ndbno", SqlDbType.NVarChar, 8).Value = lblNdbno.Value;
                    command1.Parameters.Add("@nrf6", SqlDbType.Decimal).Value = lblIndexResult.Text;
                    command1.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = lblName.Value;
                    command1.Parameters.Add("@loginid", SqlDbType.Int).Value = 100; // adminloginid
                    command1.Parameters.Add("@protein", SqlDbType.Decimal).Value = txtprotein.Text;
                    command1.Parameters.Add("@fiber", SqlDbType.Decimal).Value = txtfiber.Text;
                    command1.Parameters.Add("@va", SqlDbType.Decimal).Value = txtva.Text;
                    command1.Parameters.Add("@vc", SqlDbType.Decimal).Value = txtvc.Text;
                    command1.Parameters.Add("@calcium", SqlDbType.Decimal).Value = txtcalcium.Text;
                    command1.Parameters.Add("@iron", SqlDbType.Decimal).Value = txtiron.Text;
                    command1.Parameters.Add("@satfat", SqlDbType.Decimal, 20).Value = txtsatfat.Text;
                    command1.Parameters.Add("@sugar", SqlDbType.Decimal).Value = txtsugar.Text;
                    command1.Parameters.Add("@sodium", SqlDbType.Decimal).Value = txtsodium.Text;
                    command1.Parameters.Add("@calories", SqlDbType.Decimal).Value = txtcalories.Text;
                    command1.Parameters.Add("@lastupdatedby", SqlDbType.VarChar,20).Value = "Yihui Zhou";
                    command1.Parameters.Add("@lastupdated", SqlDbType.Date).Value= DateTime.Now;

                    command1.ExecuteNonQuery();
                    connection.Close();

                }
            }




            // Check if the ceres item is null. If ceres item doesn't exist prompt the user to open ceres and enter it there first
            //	if (txtCeresNumber.Text == "" || txtCeresDescription.Text == "") {
            //		using (SqlConnection connection = new SqlConnection(ConnectionString)) {
            //			SqlCommand command1 = new SqlCommand();
            //			command1.Connection = connection;
            //			command1.CommandType = System.Data.CommandType.Text;

            //			//if (FoodItem.newFood.name.Length > 48)
            //			//{
            //			//    FoodItem.newFood.name = FoodItem.newFood.name.Substring(0, 48);
            //			//}
            //			if (savedFoodGroup == "") { savedFoodGroup = "BRND"; }

            //			command1.CommandText = @"
            //				INSERT INTO [wholesomeDB].[dbo].[Wholesome_Item] (
            //					[NDB_No],
            //					[nrf6],
            //					[FdGrp_CD],
            //					[LastUpdatedBy],
            //					[LastUpdated],
            //					[Description 2]
            //				) VALUES (
            //					@ndbno,
            //					@nrf6,
            //					@FdGrp_CD,
            //					@lastupdatedby,
            //					@lastupdated,
            //					@Description2
            //				)
            //			";
            //			command1.Parameters.Add("@ndbno", SqlDbType.NVarChar, 8).Value = savedNdb_no;
            //			command1.Parameters.Add("@Description2", SqlDbType.NVarChar, 50).Value = lblName.Value;
            //			command1.Parameters.Add("@nrf6", SqlDbType.Decimal).Value = savedNrf6;
            //			command1.Parameters.Add("@FdGrp_CD", SqlDbType.NVarChar, 4).Value = savedFoodGroup;
            //			command1.Parameters.Add("@lastupdatedby", SqlDbType.NVarChar, 20).Value = "Nathan Hamrick";
            //			command1.Parameters.Add("@lastupdated", SqlDbType.Date).Value = DateTime.Now;

            //			connection.Open();
            //			command1.ExecuteNonQuery();
            //			connection.Close();
            //		}
            //	} else {
            //		using (SqlConnection connection = new SqlConnection(ConnectionString))
            //		{
            //			SqlCommand command1 = new SqlCommand();
            //			command1.Connection = connection;
            //			command1.CommandType = System.Data.CommandType.Text;

            //			//if (FoodItem.newFood.name.Length > 48)
            //			//{
            //			//    FoodItem.newFood.name = FoodItem.newFood.name.Substring(0, 48);
            //			//}


            //			command1.CommandText = @"UPDATE [wholesomeDB].[dbo].[Wholesome_Item] SET ndb_no = @ndbno, nrf6 = @nrf6, FdGrp_CD = @FdGrp_CD,"
            //			+ " LastUpdatedBy = @LastUpdatedBy, LastUpdated = @LastUpdated, [description 2] = @description2 WHERE No_ = @No_";

            //			command1.Parameters.Add("@No_", SqlDbType.NVarChar, 20).Value = txtCeresNumber.Text;
            //			command1.Parameters.Add("@ndbno", SqlDbType.NVarChar, 8).Value = FoodItem.newFood.ndbNo;
            //			command1.Parameters.Add("@description2", SqlDbType.VarChar, 50).Value = FoodItem.newFood.name;
            //			command1.Parameters.Add("@FdGrp_CD", SqlDbType.VarChar, 4).Value = FoodItem.newFood.foodGroup;
            //			command1.Parameters.Add("@nrf6", SqlDbType.Decimal).Value = FoodItem.newFood.NRF6;
            //			command1.Parameters.Add("@lastupdatedby", SqlDbType.NVarChar, 20).Value = "Nathan Hamrick";
            //			command1.Parameters.Add("@lastupdated", SqlDbType.Date).Value = DateTime.Now;

            //			connection.Open();
            //			command1.ExecuteNonQuery();
            //			connection.Close();
            //		}
            //	}

            //	savedNdb_no = "";

        }

        protected void btnSaveItem_Click(object sender, EventArgs e)
        {

            String ConnectionString = ConfigurationManager.ConnectionStrings["constr2"].ConnectionString;

            if (txtCeresNumber.Text == "" || txtCeresDescription.Text == "")
            {

                Response.Write("<script>alert('Please enter a value for ceres item number and description');</script>");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    //String name = lblName.Text;
                    {
                        SqlCommand command1 = new SqlCommand();
                        command1.Connection = connection;
                        command1.CommandType = System.Data.CommandType.Text;

                        //if (lblName.Text.Length > 48)
                        //{
                        //    name = lblName.Text.Substring(0, 48);
                        //}

                        command1.CommandText = @"INSERT INTO [wholesomeDB].[dbo].[Wholesome_Item] ([NDB_No], [Name], [Description], [ND_Score], [Ceres_Item_Number], [UserID], [LastUpdatedBy], [LastUpdated]) VALUES
                                      (@ndbno, @name,  @ceresdescription, @nrf6, @ceresitemnumber, @userID, @lastupdatedby, @lastupdated)";

                        command1.Parameters.Add("@ndbno", SqlDbType.NVarChar, 8).Value = FoodItem.newFood.ndbNo;
                        command1.Parameters.Add("@name", SqlDbType.VarChar, 500).Value = FoodItem.newFood.name;
                        command1.Parameters.Add("@ceresdescription", SqlDbType.VarChar, 50).Value = FoodItem.newFood.name;
                        command1.Parameters.Add("@ceresitemnumber", SqlDbType.NVarChar, 20).Value = txtCeresNumber.Text;
                        command1.Parameters.Add("@nrf6", SqlDbType.Decimal).Value = FoodItem.newFood.NRF6;
                        command1.Parameters.Add("@userID", SqlDbType.Int).Value = "1";
                        command1.Parameters.Add("@lastupdatedby", SqlDbType.NVarChar, 20).Value = "Nathan Hamrick";
                        command1.Parameters.Add("@lastupdated", SqlDbType.Date).Value = DateTime.Now;

                        connection.Open();
                        command1.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

    }
}