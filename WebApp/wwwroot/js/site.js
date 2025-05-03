// Dropdown

document.addEventListener('DOMContentLoaded', () => {
    const dropdowns = document.querySelectorAll('[data-type="dropdown"]');

    document.addEventListener('click', (e) => {
        let clickedDropdown = null;

        dropdowns.forEach(dropdown => {
            const targetId = dropdown.getAttribute('data-target');
            const targetElement = document.querySelector(targetId);

            if (dropdown.contains(e.target)) {
                clickedDropdown = targetElement;

                document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
                    if (openDropdown !== targetElement) {
                        openDropdown.classList.remove('dropdown-show');
                    }
                });

                targetElement.classList.toggle('dropdown-show');
            }
        });
        if (!clickedDropdown) {
            document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
                openDropdown.classList.remove('dropdown-show');
            });
        }
    });
});