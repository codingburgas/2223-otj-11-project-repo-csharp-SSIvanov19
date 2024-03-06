/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["**/*.razor", "**/*.cshtml", "**/*.html"],
    theme: {
        extend: {
            fontFamily: {
                'poppins': ['Poppins', 'sans-serif']
            },
        },
    },
    plugins: [],
}