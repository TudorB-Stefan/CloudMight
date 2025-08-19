"use client"
import { Geist, Geist_Mono } from "next/font/google";
import "./globals.css";
import Navbar from "./components/navbar";
import Drawer from "./components/drawer";
import Link from "next/link";
import {useEffect,useState} from "react";
import { AuthProvider } from "@/context/AuthContext";

const geistSans = Geist({
  variable: "--font-geist-sans",
  subsets: ["latin"],
});

const geistMono = Geist_Mono({
  variable: "--font-geist-mono",
  subsets: ["latin"],
});

export default function RootLayout({ children }) {
    return (
        <html lang="en">
            <body className={`${geistSans.variable} ${geistMono.variable} antialiased`}>
                <AuthProvider>
                    <div className="flex flex-col min-h-screen bg-[url(/mountains1.jpg)] bg-fixed bg-cover">
                        <Drawer/>
                        <div className=" bg-black/30 p-4 flex-1 min-h-screen">
                            <Navbar/>
                            {children}
                        </div>
    
                        <footer className="footer sm:footer-horizontal bg-black/60 backdrop-blur-sm text-neutral-content py-10 px-30">
                            <nav>
                                <h6 className="footer-title">Services</h6>
                                <a className="link link-hover">Branding</a>
                                <a className="link link-hover">Design</a>
                                <a className="link link-hover">Marketing</a>
                                <a className="link link-hover">Advertisement</a>
                            </nav>
                            <nav>
                                <h6 className="footer-title">Company</h6>
                                <a className="link link-hover">About us</a>
                                <a className="link link-hover">Contact</a>
                                <a className="link link-hover">Jobs</a>
                                <a className="link link-hover">Press kit</a>
                            </nav>
                            <nav>
                                <h6 className="footer-title">Legal</h6>
                                <a className="link link-hover">Terms of use</a>
                                <a className="link link-hover">Privacy policy</a>
                                <a className="link link-hover">Cookie policy</a>
                            </nav>
                        </footer>
                    </div>
                </AuthProvider>
            </body>
        </html>
    );
}
