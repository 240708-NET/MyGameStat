import type { Metadata } from "next";
import { Inter } from "next/font/google";
import "./globals.css";
import NavLinks from './NavBar/NavLink';
import Login from './Login/Login';

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "MyGameStat",
  description: "MyGameStat is a web application designed to help gamers effortlessly track, organize, and manage their video game collections. With features like progress tracking, personalized game analytics, and seamless integration with external gaming databases, MyGameStat offers a comprehensive toolset for every gaming enthusiast. Whether you're keeping tabs on your backlog, curating your wishlist, or analyzing your gaming habits, MyGameStat makes managing your game library engaging and intuitive.",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <Login />
        <NavLinks/>
        {children}
      </body>
    </html>
  );
}
