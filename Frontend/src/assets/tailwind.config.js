/** @type {import('tailwindcss').Config} */
export default {
    purge: [
        './index.html', './src/**/*.{vue,js,ts,jsx,tsx}'
    ],
    darkmode: false,
    theme: {
        container: {
            center: true,
            padding: '1rem',
        },
        extend: {},
    },
    variants: {
        extend: {},
    },
    plugins: [],
}
