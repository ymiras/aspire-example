namespace Catalog.Data;

public static class Extensions
{
    public static void UseMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
        
        context.Database.Migrate();

        DataSeeder.Seed(context);
    }
}

public static class DataSeeder
{
    public static void Seed(ProductDbContext dbContext)
    {
        if (dbContext.Products.Any())
            return;
        
        dbContext.Products.AddRange(Products);
        dbContext.SaveChanges();
    }
    
    private static IEnumerable<Product> Products => 
    [
        new Product { Name = "无线蓝牙耳机", Description = "高保真音质，降噪功能，续航长达20小时。", Price = 299.99m, ImageUrl = "/images/earbuds.jpg" },
        new Product { Name = "智能手表", Description = "支持心率监测、运动追踪和消息提醒。", Price = 899.00m, ImageUrl = "/images/smartwatch.jpg" },
        new Product { Name = "便携式充电宝", Description = "20000mAh大容量，支持双设备快充。", Price = 159.50m, ImageUrl = "/images/powerbank.jpg" },
        new Product { Name = "机械键盘", Description = "RGB背光，青轴手感，适用于游戏和打字。", Price = 499.99m, ImageUrl = "/images/keyboard.jpg" },
        new Product { Name = "高清网络摄像头", Description = "1080P高清视频，适用于视频会议和直播。", Price = 199.00m, ImageUrl = "/images/webcam.jpg" },
        new Product { Name = "超薄笔记本支架", Description = "铝合金材质，可调节角度，提升办公舒适度。", Price = 89.90m, ImageUrl = "/images/laptop-stand.jpg" },
        new Product { Name = "无线鼠标", Description = "静音按键，人体工学设计，适用于办公和日常使用。", Price = 79.99m, ImageUrl = "/images/wireless-mouse.jpg" },
        new Product { Name = "Type-C扩展坞", Description = "支持HDMI、USB-A、SD卡等多接口扩展。", Price = 229.50m, ImageUrl = "/images/usb-c-hub.jpg" },
        new Product { Name = "桌面LED灯", Description = "护眼调光，触摸控制，适合阅读和工作。", Price = 139.00m, ImageUrl = "/images/desk-lamp.jpg" },
        new Product { Name = "降噪头戴耳机", Description = "主动降噪，环绕音效，舒适耳罩设计。", Price = 699.99m, ImageUrl = "/images/headphones.jpg" }
    ];
}