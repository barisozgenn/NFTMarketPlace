# Stage 1: Create a temporary image for dependency installation
FROM node:18-alpine AS deps

# Install the required library for compatibility
RUN apk add --no-cache libc6-compat

# Set the working directory for subsequent commands
WORKDIR /app

# Copy only the package.json and package-lock.json files to leverage Docker layer caching
COPY frontend/web-app/package*.json ./

# Install production dependencies (excluding devDependencies) to reduce image size
RUN npm install --omit-dev

# Stage 2: Build the application
FROM node:18-alpine AS builder

# Set the working directory for subsequent commands
WORKDIR /app

# Copy node_modules from the 'deps' stage to leverage Docker layer caching
COPY --from=deps /app/node_modules ./node_modules

# Copy the entire application source code
COPY frontend/web-app ./

# Disable Next.js telemetry for build
ENV NEXT_TELEMETRY_DISABLED 1

# Build the Next.js application
RUN npm run build

# Stage 3: Create the production image
FROM node:18-alpine AS runner

# Set the working directory for subsequent commands
WORKDIR /app

# Set the NODE_ENV environment variable to production
ENV NODE_ENV production

# Disable Next.js telemetry for runtime
ENV NEXT_TELEMETRY_DISABLED 1

# Add a non-root group and user for security purposes
RUN addgroup --system --gid 1001 nodejs
RUN adduser --system --uid 1001 nextjs

# Copy the built public directory, package.json, and next.config.js
COPY --from=builder /app/public ./public
COPY --from=builder /app/package.json ./package.json
COPY --from=builder /app/next.config.js ./next.config.js

# Copy the .next directory with proper ownership
COPY --from=builder --chown=nextjs:nodejs /app/.next/standalone ./
COPY --from=builder --chown=nextjs:nodejs /app/.next/static ./.next/static

# Switch to the non-root user for security purposes
USER nextjs

# Expose the application's port
EXPOSE 3000

# Set the environment variable for the application port
ENV PORT 3000

# Run the Next.js application using the server.js file
CMD ["node", "server.js"]
