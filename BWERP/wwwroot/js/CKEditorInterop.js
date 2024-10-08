﻿window.CKEditorInterop = (() => {
    const editors = {};
    let uploadToUrl = 'http://localhost:44301/api/q/Upload';

    class MyUploadAdapter {
        constructor(loader) {
            this.loader = loader;
        }

        upload() {
            return this.loader.file
                .then(file => new Promise((resolve, reject) => {
                    this._initRequest();
                    this._initListeners(resolve, reject, file);
                    this._sendRequest(file);
                }));
        }

        abort() {
            if (this.xhr) {
                this.xhr.abort();
            }
        }

        _initRequest() {
            const xhr = this.xhr = new XMLHttpRequest();
            xhr.open('POST', uploadToUrl, true);
            xhr.responseType = 'json';
        }

        _initListeners(resolve, reject, file) {
            const xhr = this.xhr;
            const loader = this.loader;
            const genericErrorText = `Couldn't upload file: ${file.name}.`;

            xhr.addEventListener('error', () => reject(genericErrorText));
            xhr.addEventListener('abort', () => reject());
            xhr.addEventListener('load', () => {
                const response = xhr.response;

                if (!response || response.error) {
                    return reject(response && response.error ? response.error.message : genericErrorText);
                }

                resolve({
                    default: response.url
                });
            });

            if (xhr.upload) {
                xhr.upload.addEventListener('progress', evt => {
                    if (evt.lengthComputable) {
                        loader.uploadTotal = evt.total;
                        loader.uploaded = evt.loaded;
                    }
                });
            }
        }

        _sendRequest(file) {
            const data = new FormData();
            data.append('upload', file);
            this.xhr.send(data);
        }
    }

    function MyCustomUploadAdapterPlugin(editor) {
        editor.plugins.get('FileRepository').createUploadAdapter = (loader) => {
            return new MyUploadAdapter(loader);
        };
    }

    return {
        init(id, uploadImageUrl, dotNetReference) {
            ClassicEditor
                .create(document.getElementById(id), {
                    toolbar: {
                        items: [
                            'heading', 'bold', 'italic', 'underline', 'link',
                            'bulletedList', 'numberedList', '|', 
                            'outdent', 'indent', '|',
                            'alignment',
                            /*'fontBackgroundColor',*/
                            'fontFamily', 'fontSize', 'fontColor', 
                            //'imageUpload', 'imageInsert', 
                            'insertTable', 'removeFormat',
                            'undo', 'redo', '|', 'sourceEditing'
                        ]
                    },
                    language: 'en',
                    image: {
                        toolbar: [
                            'imageTextAlternative', 'imageStyle:full', 'imageStyle:side'
                        ]
                    },
                    table: {
                        contentToolbar: [
                            'tableColumn', 'tableRow', 'mergeTableCells',
                            'tableProperties', 'tableCellProperties'
                        ]
                    },
                    licenseKey: '',
                    extraPlugins: [MyCustomUploadAdapterPlugin]
                })
                .then(editor => {
                    editors[id] = editor;
                    editor.model.document.on('change:data', () => {
                        let data = editor.getData();

                        const el = document.createElement('div');
                        el.innerHTML = data;
                        if (el.innerText.trim() === '') data = null;

                        dotNetReference.invokeMethodAsync('EditorDataChanged', data);
                    });
                })
                .catch(error => console.error(error));

            if (uploadImageUrl !== null)
                uploadToUrl = uploadImageUrl;
        },
        destroy(id) {
            editors[id].destroy()
                .then(() => delete editors[id])
                .catch(error => console.log(error));
        }
    };
})();
