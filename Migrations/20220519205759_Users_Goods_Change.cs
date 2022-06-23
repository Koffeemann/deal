using Microsoft.EntityFrameworkCore.Migrations;

namespace deal.Migrations
{
    public partial class Users_Goods_Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("status",
                "Users");
            migrationBuilder.DropColumn("category",
                "Goods");

            migrationBuilder.AddColumn<bool>("IsOrganization", "Users", type: "boolean", defaultValue: false);
            migrationBuilder.AddColumn<int>("count", "Goods", type: "integer", nullable: false, defaultValue: 1);
            migrationBuilder.AddColumn<int>("category", "Goods", type: "integer", nullable: true, defaultValue: 0);
            migrationBuilder.AddForeignKey("FK_Goods_Category","Goods","category","Categories", principalColumn:"Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
