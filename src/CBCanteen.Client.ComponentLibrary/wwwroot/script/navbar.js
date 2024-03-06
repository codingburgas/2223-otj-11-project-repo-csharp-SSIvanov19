let open = false;
function initNavbar() {
    const dropdown = document.getElementById('user-menu-button');
    const dropdownWindow = document.getElementById('navbarDropdown')
    
    if (dropdown != null) {
        dropdown.addEventListener('click', (event) => {
            if (open) {
                dropdownWindow.classList.add('hidden');
            } else {
                dropdownWindow.classList.remove('hidden');
            }

            open = !open;
        });

        dropdown.addEventListener('blur', (event) => {
            dropdownWindow.classList.add('hidden');
            open = false;
        });
    }
}
