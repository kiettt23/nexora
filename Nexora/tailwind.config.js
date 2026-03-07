/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./Views/**/*.cshtml", "./wwwroot/js/**/*.js"],
  theme: {
    extend: {
      fontFamily: {
        sans: ['"Be Vietnam Pro"', "sans-serif"],
      },
      animation: {
        float: "float 6s ease-in-out infinite",
        shimmer: "shimmer 3s ease-in-out infinite",
        "slide-up": "slideUp 0.6s ease-out",
        "fade-in": "fadeIn 0.8s ease-out",
      },
    },
  },
  plugins: [require("daisyui"), require("@tailwindcss/typography")],
  daisyui: {
    themes: [
      {
        "nexora-dark": {
          primary: "#3B82F6",
          "primary-content": "#FFFFFF",
          secondary: "#8B5CF6",
          accent: "#06B6D4",
          neutral: "#18181B",
          "base-100": "#09090B",
          "base-200": "#18181B",
          "base-300": "#27272A",
          "base-content": "#FAFAFA",
          info: "#38BDF8",
          success: "#4ADE80",
          warning: "#FBBF24",
          error: "#F87171",
        },
      },
      {
        "nexora-light": {
          primary: "#2563EB",
          "primary-content": "#FFFFFF",
          secondary: "#7C3AED",
          accent: "#0891B2",
          neutral: "#18181B",
          "base-100": "#FAFAFA",
          "base-200": "#F4F4F5",
          "base-300": "#E4E4E7",
          "base-content": "#18181B",
          info: "#38BDF8",
          success: "#4ADE80",
          warning: "#FBBF24",
          error: "#F87171",
        },
      },
    ],
    darkTheme: "nexora-dark",
  },
};
