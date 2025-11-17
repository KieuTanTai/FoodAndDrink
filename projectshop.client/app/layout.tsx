import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import { MessageModalProvider } from "@/contexts/message/MessageModalProvider";
import ModalSetup from "@/components/ModalSetup";
import "./globals.css"
import { ReactNode } from "react";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export const metadata: Metadata = {
  title: "Mobile Phone Store - localhost",
  description: "Your trusted mobile phone store with the best deals",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
        <ModalSetup />
        <div id="root" className="flex flex-col w-full h-full">
          <MessageModalProvider headerId="header-container">
            {children}
          </MessageModalProvider>
        </div>
      </body>
    </html>
  );
}
