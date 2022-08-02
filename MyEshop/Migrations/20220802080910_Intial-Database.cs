using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEshop.Migrations
{
    public partial class IntialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "Money", nullable: false),
                    QuantityInStock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Items_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Items",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryToProducts",
                columns: table => new
                {
                    CategoryID = table.Column<int>(nullable: false),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryToProducts", x => new { x.ProductID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryToProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Asp.Net", "Asp.Net" },
                    { 2, "لباس ورزشی", "لباس ورزشی" },
                    { 3, "لوازم خانگی", "لوازم خانگی" },
                    { 4, "ساعت مچی", "ساعت مچی" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ID", "Price", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, 854.0m, 5 },
                    { 2, 3302.0m, 8 },
                    { 3, 2500m, 3 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ItemID", "Name" },
                values: new object[] { 1, "آموزش Asp.Net Core 3 پروژه محور ASP.NET Core بر پایه‌ی NET Core.استوار است و نگارشی از NET.محسوب می شود که مستقل از سیستم عامل و بدون واسط برنامه نویسی ویندوز عمل می کند.ویندوز هنوز هم سیستم عاملی برتر به حساب می آید ولی برنامه های وب نه تنها روز به روز از کاربرد و اهمیت بیشتری برخوردار می‌شوند بلکه باید بر روی سکوهای دیگری مانند فضای ابری(Cloud) هم بتوانند میزبانی(Host) شوند، مایکروسافت با معرفی ASP.NET Core گستره کارکرد NET.را افزایش داده است.به این معنی که می‌توان برنامه‌های کاربردی ASP.NET Core را بر روی بازه‌ی گسترده ای از محیط‌های مختلف میزبانی کرد هم‌اکنون می‌توانید پروژه های وب را برای Linux یا macOS هم تولید کنید.", 1, "آموزش Asp.Net Core 3 پروژه محور" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ItemID", "Name" },
                values: new object[] { 2, "در سال های گذشته ، کمپانی مایکروسافت با معرفی تکنولوژی های جدید و حرفه ای در زمینه های مختلف ، عرصه را برای سایر کمپانی ها تنگ تر کرده است. اخیرا این غول فناوری با معرفی فریم ورک های ASP.NET Core و همینطور Blazor قدرت خود در زمینه ی وب را به اثبات رسانده است.", 2, "آموزش Blazor از مقدماتی تا پیشرفته" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Description", "ItemID", "Name" },
                values: new object[] { 3, "آموزش اپلیکیشن های وب پیش رونده ( PWA ) آموزش PWA از مقدماتی تا پیشرفته وب اپلیکیشن‌های پیش رونده(PWA) نسل جدید اپلیکیشن‌های تحت وب هستند که می‌توانند آینده‌ی اپلیکیشن‌های موبایل را متحول کنند.در این دوره به طور جامع به بررسی آن‌ها خواهیم پرداخت. مزایا و فاکتور هایی که یک وب اپلیکیشن دارا می باشد به صورت زیر می باشد : ریسپانسیو :  رکن اصلی سایت برای PWA ریسپانسیو بودن اپلیکیشن هستش که برای صفحه نمایش کاربران مختلف موبایل و تبلت خود را وفق دهند.", 3, "آموزش اپلیکیشن های وب پیش رونده ( PWA )" });

            migrationBuilder.InsertData(
                table: "CategoryToProducts",
                columns: new[] { "ProductID", "CategoryID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryToProducts_CategoryID",
                table: "CategoryToProducts",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ItemID",
                table: "Products",
                column: "ItemID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryToProducts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
