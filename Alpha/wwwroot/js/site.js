document.addEventListener("DOMContentLoaded", () => {
    console.log("JS Loaded");

    // Form Validation
    document.querySelectorAll("form").forEach(form => {
        form.addEventListener("submit", function (e) {
            let valid = true;
            form.querySelectorAll("input[required], select[required]").forEach(input => {
                if (!input.value.trim()) {
                    input.classList.add("is-invalid");
                    valid = false;
                } else {
                    input.classList.remove("is-invalid");
                }
            });
            if (!valid) e.preventDefault();
        });
    });

    // Hantera modaler
    const modalButtons = document.querySelectorAll('[data-modal="true"]');
    modalButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modalTarget = button.getAttribute('data-target');
            const modal = document.querySelector(modalTarget);
            if (modal) modal.style.display = 'flex';
        });
    });

    const closeButtons = document.querySelectorAll('[data-close="true"]');
    closeButtons.forEach(button => {
        button.addEventListener('click', () => {
            const modal = button.closest('.modal');
            if (modal) {
                modal.style.display = 'none';
                modal.querySelectorAll('form').forEach(form => {
                    form.reset();
                    const imagePreview = form.querySelector('.image-preview');
                    if (imagePreview) imagePreview.src = '';
                    const imagePreviewer = form.querySelector('.image-previewer');
                    if (imagePreviewer) imagePreviewer.classList.remove('selected');
                });
            }
        });
    });

    //Dropdown logik
    const toggle = document.getElementById("account-toggle");
    const dropdown = document.getElementById("account-dropdown");

    if (toggle && dropdown) {
        let ignoreClick = false;

        // Klick ska toggla dropdown
        toggle.addEventListener("click", (e) => {
            e.stopPropagation();

            const isOpen = dropdown.classList.contains("show");

            // Close all other dropdowns
            document.querySelectorAll(".dropdown-menu.show").forEach(el => {
                el.classList.remove("show");
            });

            if (!isOpen) {
                dropdown.classList.add("show");
   

                //Prevent immediate close from outside click
                ignoreClick = true;
                setTimeout(() => {
                    ignoreClick = false;
                }, 50);
            } else {
                dropdown.classList.remove("show");
                
            }
        });

        //Prevent click inside dropdown from closing it
        dropdown.addEventListener("click", (e) => {
            e.stopPropagation();
        });

        // Stäng dropdowns när man klickar utanför
        document.addEventListener("click", (e) => {
            if (ignoreClick) return;

            if (!dropdown.contains(e.target) && !toggle.contains(e.target)) {
                if (dropdown.classList.contains("show")) {
                    dropdown.classList.remove("show");
                    console.log("🧊 Closed dropdown from outside click");
                }
            }
        });

        console.log("✅ Account dropdown events attached");
    }


    // Projectcard dropdown
    document.querySelectorAll(".btn-more").forEach(button => {
        button.addEventListener("click", function (e) {
            e.stopPropagation();

            // Close all other dropdowns
            document.querySelectorAll(".dropdown-menu.show").forEach(menu => {
                menu.classList.remove("show");
                menu.closest(".more-icon")?.querySelector(".btn-more")?.classList.remove("active");
            });

            const id = button.getAttribute("data-project-id");
            const dropdown = document.getElementById(`dropdown-${id}`);
            if (dropdown) {
                const isOpen = dropdown.classList.toggle("show");
                button.classList.toggle("active", isOpen); 
            }
        });
    });

    // Stäng dropdowns när man klickar utanför
    document.addEventListener("click", () => {
        document.querySelectorAll(".dropdown-menu.show").forEach(menu => {
            menu.classList.remove("show");

            
            const button = menu.closest(".more-icon")?.querySelector(".btn-more");
            if (button) {
                button.classList.remove("active");
            }
        });
    });


    // For Add modal
    initializeQuill("quill-editor-add", "Description");


});


//Chatgpt genererad kod för att få min quill att fungera
    function initializeQuill(editorId, hiddenFieldName, initialContent = '') {
        const quill = new Quill(`#${editorId}`, {
            theme: 'snow',
            placeholder: 'Type something', 
            modules: {
                toolbar: [
                    ['bold', 'italic', 'underline'],
                    [{ 'align': '' }, { 'align': 'center' }, { 'align': 'right' }],
                    [{ 'list': 'ordered' }, { 'list': 'bullet' }],
                    ['link']
                ]
            }
        });

        
        if (initialContent) {
            quill.root.innerHTML = initialContent;
        }

        const form = document.querySelector(`#${editorId}`).closest('form');
        if (form) {
            form.addEventListener('submit', () => {
                const html = quill.root.innerHTML.trim();
                form.querySelector(`input[name="${hiddenFieldName}"]`).value = html;
            });
        }
    }
   

   