window.initializeSidePanel = function () {
    const sidePanelToggler = document.getElementById('sidepanel-toggler');
    const sidePanel = document.getElementById('app-sidepanel');
    const sidePanelDrop = document.getElementById('sidepanel-drop');
    const sidePanelClose = document.getElementById('sidepanel-close');

    // Responsive side panel handling
    if (sidePanel) {
        function responsiveSidePanel() {
            let w = window.innerWidth;
            console.log('Window width:', w);
            if (w >= 1200) {
                sidePanel.classList.remove('sidepanel-hidden');
                sidePanel.classList.add('sidepanel-visible');
            } else {
                sidePanel.classList.remove('sidepanel-visible');
                sidePanel.classList.add('sidepanel-hidden');
            }
        }

        window.addEventListener('load', responsiveSidePanel);
        window.addEventListener('resize', responsiveSidePanel);
        responsiveSidePanel();
    }

    // Toggle side panel visibility
    if (sidePanelToggler) {
        sidePanelToggler.addEventListener('click', (e) => {
            e.preventDefault(); // Prevent default action
            if (sidePanel.classList.contains('sidepanel-visible')) {
                sidePanel.classList.remove('sidepanel-visible');
                sidePanel.classList.add('sidepanel-hidden');
            } else {
                sidePanel.classList.remove('sidepanel-hidden');
                sidePanel.classList.add('sidepanel-visible');
            }
        });
    }

    // Close side panel
    if (sidePanelClose) {
        sidePanelClose.addEventListener('click', (e) => {
            e.preventDefault(); // Prevent default action
            if (sidePanelToggler) sidePanelToggler.click();
        });
    }

    // Handle side panel backdrop click
    if (sidePanelDrop) {
        sidePanelDrop.addEventListener('click', (e) => {
            e.preventDefault(); // Prevent default action
            if (sidePanelToggler) sidePanelToggler.click();
        });
    }

    // Search box toggle for mobile
    const searchMobileTrigger = document.querySelector('.search-mobile-trigger');
    const searchBox = document.querySelector('.app-search-box');

    if (searchMobileTrigger && searchBox) {
        searchMobileTrigger.addEventListener('click', (e) => {
            e.preventDefault(); // Prevent default action
            searchBox.classList.toggle('is-visible');

            let searchMobileTriggerIcon = document.querySelector('.search-mobile-trigger-icon');

            if (searchMobileTriggerIcon) {
                if (searchMobileTriggerIcon.classList.contains('fa-search')) {
                    searchMobileTriggerIcon.classList.remove('fa-search');
                    searchMobileTriggerIcon.classList.add('fa-times');
                } else {
                    searchMobileTriggerIcon.classList.remove('fa-times');
                    searchMobileTriggerIcon.classList.add('fa-search');
                }
            }
        });
    }
}
