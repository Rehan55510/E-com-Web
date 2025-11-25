// Mobile Menu Toggle
document.addEventListener('DOMContentLoaded', function () {
    // Update cart count
    updateCartCount();

    // Mobile menu toggle
    const mobileMenuBtn = document.getElementById('mobile-menu-toggle');
    const navMenu = document.querySelector('.nav-menu');

    if (mobileMenuBtn && navMenu) {
        mobileMenuBtn.addEventListener('click', function () {
            navMenu.classList.toggle('active');
            const isExpanded = navMenu.classList.contains('active');
            mobileMenuBtn.setAttribute('aria-expanded', isExpanded);
            mobileMenuBtn.innerHTML = isExpanded ? '✕' : '☰';
        });

        // Close menu when clicking outside
        document.addEventListener('click', function (event) {
            if (!event.target.closest('.navbar')) {
                navMenu.classList.remove('active');
                mobileMenuBtn.setAttribute('aria-expanded', 'false');
                mobileMenuBtn.innerHTML = '☰';
            }
        });

        // Close menu when clicking a link
        navMenu.querySelectorAll('a').forEach(link => {
            link.addEventListener('click', function () {
                navMenu.classList.remove('active');
                mobileMenuBtn.setAttribute('aria-expanded', 'false');
                mobileMenuBtn.innerHTML = '☰';
            });
        });
    }
});

function updateCartCount() {
    const cart = JSON.parse(sessionStorage.getItem('Cart') || '{}');
    const totalItems = Object.values(cart).reduce((sum, item) => sum + (item.Quantity || 0), 0);
    const cartCountElement = document.getElementById('cart-count');
    if (cartCountElement) {
        cartCountElement.textContent = totalItems;
    }
}
