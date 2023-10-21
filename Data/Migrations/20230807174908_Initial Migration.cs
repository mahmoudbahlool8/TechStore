using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "https://scontent.fcai20-1.fna.fbcdn.net/v/t39.30808-6/221503802_365410454968086_4381944777291518500_n.jpg?_nc_cat=106&cb=99be929b-3346023f&ccb=1-7&_nc_sid=09cbfe&_nc_eui2=AeFwFGL-TCw4pBckVEc35vpBToZ8A_i0qGNOhnwD-LSoY4ZbrmxEYi3k3Z5AO98gF3jLyWTd5XSGlge9Ci1NLXI9&_nc_ohc=3KYkntxmV8MAX_1P6vi&_nc_ht=scontent.fcai20-1.fna&oh=00_AfCB00pV_MaVR8WrbNUhADGcajNW8TOmj4znupu-pL3JPA&oe=64D6EC49")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
