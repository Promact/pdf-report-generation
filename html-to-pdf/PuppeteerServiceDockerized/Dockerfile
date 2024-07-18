# Use the official Node.js image
FROM node:22

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Install Chromium dependencies
RUN apt-get update && apt-get install -y \
    wget \
    ca-certificates \
    fonts-liberation \
    libasound2 \
    libatk-bridge2.0-0 \
    libatk1.0-0 \
    libcups2 \
    libdrm2 \
    libxkbcommon-x11-0 \
    libxrandr2 \
    xdg-utils \
    libxshmfence1 \
    libgbm-dev \
    libnss3 \
    libxss1 \
    libxcomposite1 \
    libxcursor1 \
    libxdamage1 \
    libxi6 \
    libxtst6 \
    libglib2.0-0 \
    libgtk-3-0 \
    libgdk-pixbuf2.0-0 \
    libpango1.0-0 \
    libgl1-mesa-glx \
    --no-install-recommends && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Copy the rest of the application code
COPY . .

# Expose the port the app runs on
EXPOSE 3000

# Command to run the application
CMD ["node", "index.js"]