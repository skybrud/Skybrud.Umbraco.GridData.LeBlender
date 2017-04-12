Skybrud.Umbraco.GridData.LeBlender
==================================

**Skybrud.Umbraco.GridData.LeBlender** is an addon for [**Skybrud.Umbraco.GridData**](https://github.com/skybrud/Skybrud.Umbraco.GridData) which adds support for controls using the [**LeBlender**](https://github.com/Lecoati/LeBlender) package.

## Installation

1. [**NuGet Package**][NuGetPackage]  
Install this NuGet package in your Visual Studio project. Makes updating easy.

1. [**ZIP file**][GitHubRelease]  
Grab a ZIP file of the latest release; unzip and move the contents to the root directory of your web application.

<!--1. [**Umbraco package**][UmbracoPackage]  
Install the package through the Umbraco backoffice.-->

[NuGetPackage]: https://www.nuget.org/packages/Skybrud.Umbraco.GridData.LeBlender
[UmbracoPackage]: https://our.umbraco.org/projects/developer-tools/skybrudumbracogriddataleblender/
[GitHubRelease]: https://github.com/skybrud/Skybrud.Umbraco.GridData.LeBlender

## Usage
Skybrud.Umbraco.GridData relies on a number of converters to provide the strongly typed models for the grid. This package adds a new converter to do just that for controls using LeBlender.

For the converter to handle your controls, the editor alias must start with `LeBlender.` - eg. `LeBlender.Spotbox` or `LeBlender.Contact`.

If a control matches this criteria, the `Value` property of `GridControl` will be an instance of `GridControlLeBlenderValue`, while `Editor.Config` will be an instance of `GridEditorLeBlenderConfig`.

A partial view for rendering the entire grid model could look like this:

```C#
@using Skybrud.Umbraco.GridData
@inherits UmbracoViewPage<GridDataModel>

@{

    // Stop any further rendering since the model isn't valid
    if (!Model.IsValid) { return; }

    // Compared to "Fanoe.cshtml" from the Fanoe starter kit, it will check and handle in different ways if is just a
    // single section or multiple sections. This examples doesn't since the Umbraco UI doesn't really allow users to
    // add more than one section.

    <div class="umb-grid">
        @foreach (GridSection section in Model.Sections) {
            <div class="grid-section">
                @foreach (GridRow row in section.Rows.Where(x => x.IsValid)) {
                    <div class="grid-row">
                        @foreach (GridArea area in row.Areas) {
                            foreach (GridControl control in area.Controls) {

                                switch (control.Editor.Alias) {

                                    case "LeBlender.ContactPerson": {
                                        @control.GetHtml(Html, "TypedGrid/Editors/LeBlender/ContactPerson")
                                        break;
                                    }
                                    
                                    // other editors omitted for simplicty
  
                                }

                            }
                        }
                    </div>
                }
            </div>
        }
    </div>

}
```

And the partial view for rendering the contact person could look like:

```C#
@using Lecoati.LeBlender.Extension.Models
@using Umbraco.Web.Models
@using Skybrud.Umbraco.GridData.Rendering

@inherits UmbracoViewPage<GridControlWrapper<Skybrud.Umbraco.GridData.LeBlender.Values.GridControlLeBlenderValue>>

@foreach (LeBlenderValue value in Model.Value.Items) {
    
    string name = value.GetValue<string>("name");
    string email = value.GetValue<string>("email");

    IPublishedContent image = Umbraco.TypedMedia(value.GetValue<int>("image"));

    <div>
        <div style="display: flex; margin-top: 15px;">
            <div style="width: 150px; min-height: 35px; margin-right: 10px;">
                <img src="@image.GetCropUrl(150, 150, preferFocalPoint: true, imageCropMode: ImageCropMode.Crop, upScale: false)" />
            </div>
            <div>
                <h2>@name</h2>
                <div class="inner">
                    @email
                </div>
            </div>
        </div>
    </div>

}
```