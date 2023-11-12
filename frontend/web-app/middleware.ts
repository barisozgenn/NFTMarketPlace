export { default } from "next-auth/middleware"
//to secure pages and API routes and we can effectively use Next.js middleware
//it has to be in the root of our application and named exactly middleware.ts
//So our session dashboard would be an example of something that we do not want anonymous users to access.
export const config = {
    matcher: [
        '/session'
    ],
    pages: {
        signIn: '/api/auth/signin'
    }
}