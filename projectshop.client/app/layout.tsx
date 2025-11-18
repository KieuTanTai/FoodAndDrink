import type { Metadata } from "next";
import { Geist, Geist_Mono } from "next/font/google";
import { MessageModalProvider } from "@/contexts/message/MessageModalProvider";
import ModalSetup from "@/components/ModalSetup";
import "./globals.css";
import { ReactNode } from "react";
import Header from "@/components/HeaderComponents";
import SubHeader from "@/components/SubHeader";
import Footer from "@/components/FooterComponents";

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
      <body
        className={`${geistSans.variable} ${geistMono.variable} antialiased`}
      >
        <ModalSetup />
        <MessageModalProvider headerId="header-container">
          <div id="root" className="flex flex-col w-full h-full">
            <header id="header" className="sticky top-0 z-50">
              <Header />
              <SubHeader />
            </header>

            {children}

            <footer id="footer" className="mt-10">
              <Footer />
            </footer>
          </div>
        </MessageModalProvider>
      </body>
    </html>
  );
}
