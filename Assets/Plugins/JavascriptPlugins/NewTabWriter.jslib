mergeInto(LibraryManager.library, {

  WriteToNewTab: function (str) {
    var tab = window.open('about:blank', '_blank');
    tab.document.write(Pointer_stringify(str));
    tab.document.close(); 
  },

});