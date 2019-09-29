/*
This is a very simple plain CAKE file to easily integrate our project to any CI environments.
Tasks below can be extended depending on the needs.
*/

var target = Argument("target", "Test");

Task("Build")
  .Does(() =>
{
  Information("Building the app!");
  DotNetCoreBuild("./src/SmartLock.sln");
});

Task("Test")
   .IsDependentOn("Build")
   .Does(() =>
{
  Information("Running tests!");
  var settings = new DotNetCoreTestSettings
     {
         Configuration = "Release"
     };

  DotNetCoreTest("./src/SmartLock.Test/SmartLock.Test.csproj", settings);

});

RunTarget(target);
