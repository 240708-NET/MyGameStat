'use client';

import React, { useState } from 'react';
import Link from 'next/link';
import { usePathname } from 'next/navigation';
import { HomeIcon, FolderOpenIcon, ChartPieIcon, HeartIcon, Bars3Icon, XMarkIcon } from '@heroicons/react/24/outline';
import styles from './NavBarStyle.module.css';

const links = [
  { name: 'Home', href: '/', icon: HomeIcon },
  { name: 'Collection', href: '/Pages/Collection', icon: FolderOpenIcon },
  { name: 'Analytics', href: '/Pages/Analytics', icon: ChartPieIcon },
  { name: 'Wishlist', href: '/Pages/Wishlist', icon: HeartIcon },
];

export default function NavLinks() {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const pathname = usePathname();

  const toggleMenu = () => setIsMenuOpen(!isMenuOpen);

  return (
    <div className={`${styles.navContainer} ${isMenuOpen ? styles.navOpen : styles.navClosed}`}>
      <button onClick={toggleMenu} className={styles.hamburgerButton}>
        {isMenuOpen ? <XMarkIcon className={styles.hamburgerIcon} /> : <Bars3Icon className={styles.hamburgerIcon} />}
      </button>
      <ul className={`${styles.menuContainer} ${isMenuOpen ? styles.menuOpen : styles.menuClosed}`}>
        {links.map((link) => {
          const LinkIcon = link.icon;
          const isActive = pathname === link.href;

          return (
            <li key={link.name}>
              <Link href={link.href} className={isActive ? styles.active : ''}>
                <LinkIcon className={styles.icon} />
                {link.name}
              </Link>
            </li>
          );
        })}
      </ul>
    </div>
  );
}
