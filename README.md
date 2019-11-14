<div dir="rtl">
  
# دمو ابزارهای متن‌کاوی فارسی‌یار 


**توجه** این پروژه تحت وب دمو برای ابزارهای متن‌کاوی فارسی‌یار است و در ویندوز، مک و لینوکس قابل استفاده است. نسخه آنلاین آن را می‌توانید در آدرس [https://demo.text-mining.ir](demo.text-mining.ir) مشاهده کنید.

### پیش‌نیازها
برای استفاده از این پروژه به `.NET Core 2.2` نیاز دارید که از آدرس [https://dotnet.microsoft.com/download/dotnet-core/2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) برای سیستم‌عامل‌های مختلف قابل دانلود است.


### شروع به کار
برای استفاده از این پروژه، ابتدا دستور زیر را اجرا کنید.

`https://github.com/Text-Mining/demo.git`

در صورتی که git ندارید، می‌توانید نسخه `zip` پروژه را از آدرس `https://github.com/Text-Mining/demo/archive/master.zip` دانلود کنید.

سپس به آدرس [https://app.text-mining.ir](https://app.text-mining.ir) مراجعه کنید و یک `API Key` دریافت کنید. در فایل‌های دانلود شده پروژه فایل `appsettings.json` را باز کرده و در بخش `TextMiningApiKey` مقدار `API Key` دریافتی را وارد کنید. سپس کافی است در پوشه `Textmining.Demo.Web` دستور زیر را اجرا کنید:

`dotnet run`

بعد از این کار، پروژه در آدرس `http://localhost:5000` در دسترس خواهد بود

</div>
