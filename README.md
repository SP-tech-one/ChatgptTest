# Registration Demo

این مخزن یک نمونه‌ی کامل از معماری لایه‌ای مبتنی بر Domain Driven Design برای یک فرم ثبت‌نام تحت وب است که با ‎.NET 9‎ و Razor Pages پیاده‌سازی شده است. اعتبارسنجی دامنه در سمت سرور انجام می‌شود و خروجی تمیز به صورت JSON به فرانت‌اند برگردانده می‌شود تا در ‎localStorage‎ مرورگر نگهداری شود.

## ساختار راه‌حل

```
ChatgptTest/
├── RegistrationApp.sln
├── src/
│   ├── RegistrationApp.Domain/        ← موجودیت‌ها، Value Objectها و منطق دامنه
│   ├── RegistrationApp.Application/   ← سرویس‌های کاربردی و DTO ها
│   ├── RegistrationApp.Infrastructure/← پیاده‌سازی وابستگی‌ها (Clock، تعریف فرم و ...)
│   └── RegistrationApp.Web/           ← لایه‌ی نمایش Razor Pages و اسکریپت‌های سمت کلاینت
```

## اجرای پروژه

1. پیش‌نیازها: نصب ‎.NET 9 SDK‎.
2. بیلد و اجرای پروژه‌ی وب:

```bash
dotnet build RegistrationApp.sln
dotnet run --project src/RegistrationApp.Web/RegistrationApp.Web.csproj
```

در صورت اجرای پروژه، صفحه‌ی ثبت‌نام در آدرس `https://localhost:5001` (یا پورتی که در خروجی `dotnet run` اعلام می‌شود) در دسترس خواهد بود.

## قابلیت‌ها

- اعتبارسنجی کامل اطلاعات کاربر مطابق قواعد دامنه (نام، ایمیل، تاریخ تولد و ...).
- ذخیره‌ی رکوردهای تأیید شده در ‎localStorage‎ مرورگر همراه با جدول پویا و آمار رکوردها.
- ساختار ماژولار با DI و سرویس‌های قابل تست و قابل توسعه.
