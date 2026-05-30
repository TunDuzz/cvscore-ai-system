import { Link } from 'react-router-dom'

export function AccessSection() {
  return (
    <section className="access-section" id="access">
      <div className="access-copy">
        <p className="eyebrow">Start using the platform</p>
        <h2>You can browse first. Create an account only when you are ready to practice.</h2>
        <p>
          Keep the product open to visitors, then move them into a clean authentication flow when they want to save
          profiles, run sessions, and track progress.
        </p>
      </div>

      <div className="auth-card access-card">
        <div className="access-card-copy">
          <span className="preview-caption">Account access</span>
          <h3>Use separate pages for authentication, keep the home page focused on product value.</h3>
          <p>
            Visitors can understand the platform first, then move into a dedicated login or registration layout when
            they are ready to use it.
          </p>
        </div>

        <div className="access-card-actions">
          <Link className="primary-button wide" to="/register">
            Create Account
          </Link>
          <Link className="secondary-button wide" to="/login">
            Sign In
          </Link>
        </div>
      </div>
    </section>
  )
}
