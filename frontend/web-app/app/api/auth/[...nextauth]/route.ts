import NextAuth, { NextAuthOptions } from "next-auth"
import DuendeIdentityServer6 from 'next-auth/providers/duende-identity-server6';

export const authOptions: NextAuthOptions = {
    session: {
        strategy: 'jwt'
    },
    providers: [
        DuendeIdentityServer6({
            id: 'id-server',
            clientId: 'nextApp',
            clientSecret: 'secret',//which is not a very secret secret :D for development is ok
            issuer: 'http://localhost:5029',
            authorization: {params: {scope: 'openid profile nftAuctionApp'}},
            idToken: true
        })
    ],
    callbacks: {
        async jwt({token, profile, account}) {
            if (profile) {
                token.username = profile.username
            }
            if (account) {
                token.access_token = account.access_token
            }
            return token;
        },
        async session({session, token}) {
            if (token) {
                session.user.username = token.username
            }
            return session;
        }
    }
}

const handler = NextAuth(authOptions);
export { handler as GET, handler as POST }