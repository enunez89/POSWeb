tinymce.PluginManager.add('tokenizador', function (editor, url) {
    // Add a button that opens a window
    editor.addButton('tokenizador', {
        text: 'My button',
        icon: false,
        onclick: function () {
            // Open window
            editor.windowManager.open({
                title: 'Variables del Sistema',
                body: [
                    { type: 'textbox', name: 'title', label: 'Title' }
                ],
                onsubmit: function (e) {
                    // Insert content when the window form is submitted
                    editor.insertContent('Title: ' + e.data.title);
                }
            });
        }
    });

    editor.addMenuItem('tokenizador', {
        text: 'Variables del Sistema',
        context: 'insert',
        onselect: function (e) {
            editor.insertContent(this.value());
        },
        menu: [
            { text: '%Usuario.Nombre%', value: '%Usuario.Nombre%', onclick: function () { editor.insertContent('%Usuario.Nombre%'); }},
            { text: '%Usuario.Password%', value: '%Usuario.Password%', onclick: function () { editor.insertContent('%Usuario.Password%'); } },
            { text: '%Usuario.CodigoUsuario%', value: '%Usuario.CodigoUsuario%', onclick: function () { editor.insertContent('%Usuario.CodigoUsuario%'); } },
            { text: '%Usuario.Identificacion%', value: '%Usuario.Identificacion%', onclick: function () { editor.insertContent('%Usuario.Identificacion%'); } },
        ] 
    });

});
