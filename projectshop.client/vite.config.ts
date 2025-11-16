import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';
import tailwindcss from '@tailwindcss/vite';

// Đường dẫn đầy đủ tới dotnet9/dotnet
const dotnetPath = path.join(env.HOME || '', 'dotnet9', 'dotnet');

const baseFolder =
    env.APPDATA !== undefined && env.APPDATA !== ''
        ? `${env.APPDATA}/ASP.NET/https`
        : `${env.HOME}/.aspnet/https`;

const certificateName = "projectshop.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
}

if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync(dotnetPath, [
        'dev-certs',
        'https',
        '--export-path',
        certFilePath,
        '--format',
        'Pem',
        '--no-password',
    ], { stdio: 'inherit', }).status) {
        throw new Error("Could not create certificate.");
    }
}

const target = env.ASPNETCORE_HTTP_PORT ? `https://localhost:${env.ASPNETCORE_HTTP_PORT}` :
    env.ASPNETCORE_URLS
        ? env.ASPNETCORE_URLS.split(';').find(url => url.startsWith('http://')) || 'http://localhost:5294' : 'http://localhost:5294';

export default defineConfig({
    plugins: [plugin(),
    tailwindcss()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            '/api': {
                target,
                secure: false,
                changeOrigin: true,
                rewrite: (path) => path.replace(/^\/api/, '')
            }
        },
        port: parseInt(env.DEV_SERVER_PORT || '58435'),
        https: {
            key: fs.readFileSync(keyFilePath),
            cert: fs.readFileSync(certFilePath),
        }
    }
});