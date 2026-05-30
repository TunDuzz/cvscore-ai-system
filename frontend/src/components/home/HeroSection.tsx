import { Link } from 'react-router-dom'

export function HeroSection() {
  return (
    <section className="hero-section" id="home">
      <div className="hero-copy">
        <p className="eyebrow">Mock interview practice for modern candidates</p>
        <h1>Sharpen your CV, rehearse smarter, and walk into interviews with less guesswork.</h1>
        <p className="hero-description">
          CVScore AI brings CV review, role-based mock interviews, and structured feedback into one bright, focused
          workspace built for consistent practice.
        </p>

        <div className="hero-actions">
          <Link className="primary-button large" to="/register">
            Create Account
          </Link>
          <a className="secondary-button large" href="#features">
            Explore Features
          </a>
        </div>

        <div className="hero-metrics">
          <div>
            <strong>Role-aware</strong>
            <span>Interview profiles based on target position and level.</span>
          </div>
          <div>
            <strong>Fast feedback</strong>
            <span>Immediate review loops to improve each answer.</span>
          </div>
          <div>
            <strong>Public home</strong>
            <span>Visitors can learn the product before creating an account.</span>
          </div>
        </div>
      </div>

      <div className="hero-panel">
        <div className="preview-card preview-main">
          <div className="preview-label">Interview Snapshot</div>
          <h2>Frontend Engineer · Mid Level</h2>
          <p>
            Session design tuned for product thinking, React fundamentals, communication quality, and practical problem
            solving.
          </p>
          <div className="preview-score-row">
            <div>
              <span className="preview-caption">Language</span>
              <strong>English / Vietnamese</strong>
            </div>
            <div>
              <span className="preview-caption">Format</span>
              <strong>Guided mock session</strong>
            </div>
          </div>
        </div>

        <div className="preview-grid">
          <article className="preview-card">
            <span className="preview-caption">CV Focus</span>
            <strong>Clearer strengths</strong>
            <p>Highlight measurable impact and tighten narrative before interviews.</p>
          </article>
          <article className="preview-card">
            <span className="preview-caption">Answer Review</span>
            <strong>Actionable notes</strong>
            <p>See what worked, what felt weak, and how to answer better next round.</p>
          </article>
        </div>
      </div>
    </section>
  )
}
