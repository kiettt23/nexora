// Cart badge - fetch count on page load
document.addEventListener("DOMContentLoaded", function () {
  const cartCount = document.getElementById("cart-count");
  if (cartCount) {
    fetch("/Cart/Count")
      .then((r) => {
        if (r.ok && r.headers.get("content-type")?.includes("text/plain")) {
          return r.text();
        }
        // Not logged in or redirect — return "0"
        return "0";
      })
      .then((count) => {
        const num = parseInt(count, 10);
        cartCount.textContent = isNaN(num) ? "0" : num;
      })
      .catch(() => (cartCount.textContent = "0"));
  }
});

// HTMX: after cart add, update badge
document.addEventListener("htmx:afterRequest", function (e) {
  if (e.detail.pathInfo?.requestPath?.includes("/Cart/Add")) {
    const cartCount = document.getElementById("cart-count");
    if (cartCount && e.detail.xhr?.responseText) {
      const num = parseInt(e.detail.xhr.responseText, 10);
      cartCount.textContent = isNaN(num) ? "0" : num;
    }
  }
});

// Toast helper
function showToast(message, type = "success") {
  window.dispatchEvent(
    new CustomEvent("toast", {
      detail: { message, type },
    })
  );
}
