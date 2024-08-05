window.onbeforeunload = function (e) {
    localStorage.setItem('lastVisitedPage', window.location.pathname);
};

window.onload = function (e) {
    const lastVisitedPage = localStorage.getItem('lastVisitedPage');
    if (lastVisitedPage && lastVisitedPage !== '/') {
        window.history.replaceState({}, '', lastVisitedPage);
    }
};
