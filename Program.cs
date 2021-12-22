var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();





if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}



app.UseDefaultFiles();
app.UseStaticFiles();



app.UseRouting();

app.UseSession();

app.UseAuthentication();

// app.MapGet("/", () => "Hello World!");

app.Run();
