import { Link } from 'react-router-dom'

export function HomeHeader() {
  return (
    <header className="topbar">
      <a className="brand" href="#home">
        <span className="brand-mark">C</span>
        <span>CVScore AI</span>
      </a>

      <nav className="topnav">
        <a href="#features">Features</a>
        <a href="#workflow">How It Works</a>
        <a href="#access">Access</a>
      </nav>

      <div className="topbar-actions">
        <Link className="ghost-button" to="/login">
          Sign In
        </Link>
        <Link className="primary-button" to="/register">
          Get Started
        </Link>
      </div>
    </header>
  )
}
