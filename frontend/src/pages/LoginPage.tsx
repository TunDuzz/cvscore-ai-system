import { Link } from 'react-router-dom'
import { AuthLayout } from '../components/layout/AuthLayout'

export function LoginPage() {
  return (
    <AuthLayout
      eyebrow="Sign in"
      title="Welcome back to your interview workspace."
      description="Access your saved profiles, continue mock sessions, and review earlier feedback without friction."
    >
      <form className="auth-form auth-page-form">
        <div className="auth-form-header">
          <h2>Sign in</h2>
          <p>Use your account to continue practicing.</p>
        </div>

        <label>
          Email
          <input type="email" placeholder="you@example.com" />
        </label>
        <label>
          Password
          <input type="password" placeholder="Your password" />
        </label>

        <button className="primary-button wide" type="submit">
          Sign In
        </button>

        <p className="auth-switch-text">
          New here? <Link to="/register">Create an account</Link>
        </p>
      </form>
    </AuthLayout>
  )
}
