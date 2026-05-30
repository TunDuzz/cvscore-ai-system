import { Link } from 'react-router-dom'
import { AuthLayout } from '../components/layout/AuthLayout'

export function RegisterPage() {
  return (
    <AuthLayout
      eyebrow="Create account"
      title="Start practicing with a cleaner interview preparation flow."
      description="Create your account when you are ready to save profiles, launch sessions, and keep your preparation history in one place."
    >
      <form className="auth-form auth-page-form">
        <div className="auth-form-header">
          <h2>Create account</h2>
          <p>Set up your workspace in a minute.</p>
        </div>

        <label>
          Full name
          <input type="text" placeholder="Nguyen Van A" />
        </label>
        <label>
          Email
          <input type="email" placeholder="you@example.com" />
        </label>
        <label>
          Password
          <input type="password" placeholder="At least 8 characters" />
        </label>

        <button className="primary-button wide" type="submit">
          Create Account
        </button>

        <p className="auth-switch-text">
          Already have an account? <Link to="/login">Sign in</Link>
        </p>
      </form>
    </AuthLayout>
  )
}
