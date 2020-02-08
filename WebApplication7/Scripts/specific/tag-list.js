function addTags() {
    $('#tags').tagsInput({
        //have a get request for a list of tags later when we have it
        'autocomplete': {
            source: [
                'jQuery',
                'Script',
                'Net',
                'Demo',
                'People',
                'Test0',
                'Html',
                'tag',
                'Tag5'
            ]
        }
        //https://www.jqueryscript.net/form/Tags-Input-Autocomplete.html
    });
};
//tag list that can be edited here. It's separate because multiple pages use it and in case it needs to be changed separately.