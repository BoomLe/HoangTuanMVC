using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
namespace App.ExtenMethod
{
    public static  class AppExten
    {
        public static void AddErrorfix(this IApplicationBuilder app )
        {

            app.UseStatusCodePages(async Error => 
            {
                Error.Run(async context =>
                {
                    var response = context.Response;
                    var code = response.StatusCode;
                    
                     var content = @$"<!DOCTYPE html>
<html>
    <header>
         <meta charset='UTF-8'/>
        <Title> Lỗi không truy cập được {code} </Title>
        <link rel='stylesheet' href='/File/style.css'>
    </header>
    <body class='mausac'>
       <p style='background-color : red; color: yellow; padding: 50px' >Lỗi : {code} - { (HttpStatusCode ) code }</p>
    </body>
</html>";

await response.WriteAsync(content);




                });
            });


        }

    }
    
}