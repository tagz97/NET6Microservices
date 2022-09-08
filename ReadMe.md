# About
Thank you for taking the time to have a look at this repository! I will be working on it for the near future. My aim for this repository is to show how I would approach building an API and how I like to structure my projects.

# Things to note
If you clone the repository, you will not be able to run it as the NuGet for the persistance is not yet shipped as part of this project. I need to work on it in the background before bringing it forward. All other packages can be gotten from building the projects in the `Shared.sln`.

# How to generate NuGet packages from `Shared.sln`
Open the `Shared.sln` solution and build each project individually to generate the NuGet packages. They will be placed into a folder with the path `C:/NuGets` and can be found under the path of `C:NuGets/Debug`.

# How to install local NuGet packages
Right click the `Customer.API` and select the option `Manage NuGet packages...`. In the top right, you will see a field called `Package source:` with the value in the dropdown being `nuget.org`. Click the settings cog next to the dropdown to allow us to add additional sources.

Next, click the Plus button and give it a name in the bottom of the screen and for the source, browse to `C:NuGets` and select it. Click the `Update` button once you have finished to validate and then click `OK` to close. Now, when browsing you can select the package source to be the one you created using the dropdown list and you will see the relevant packages.

Here is a link to assist with this process visually: https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-visual-studio

# Coming soon
local.settings.json content

Customer.API documentation

Shared.sln documentation