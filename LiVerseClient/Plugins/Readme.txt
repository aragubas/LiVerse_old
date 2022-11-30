This folder contains all installed plugins.

A plugin directory should be set up in this structure:
<plugin_id>/Content <- Plugin Content Files (such as Audio or Images)
    /Content/Images <- Images Files
    /Content/Audio <- Music/Sound Effectd
    /Content/Fonts <- Font Files

<plugin_id>/Binaries <- Plugin binaries
<plugin_id>/plugin_metadata.json <- Plugin metadata information (example below)
 
[plugin_metadata.json]
{
    "id": "author.plugin_name",
    "title": "Beautiful Plugin Name (name shown in UI's)",
    "version": "Major.Minor.Revision",
    "author": "AuthorName, or multiple authors separated by a colon",
    "assembly_path": "plugin.dll"
}

Assembly path is always relative to the current plugin folder directory