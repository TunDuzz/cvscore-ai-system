import type { PropsWithChildren } from 'react'
import { Link } from 'react-router-dom'

type AuthLayoutProps = PropsWithChildren<{
  eyebrow: string
  title: string
  description: string
}>

export function AuthLayout({ eyebrow, title, description, children }: AuthLayoutProps) {
  return (
    <div className="auth-shell">
      <div className="ambient ambient-left" />
      <div className="ambient ambient-right" />

      <div className="auth-shell-inner">
        <section className="auth-panel auth-panel-copy">
          <Link className="brand" to="/">
            <span className="brand-mark">C</span>
            <span>CVScore AI</span>
          </Link>

          <p className="eyebrow">{eyebrow}</p>
          <h1>{title}</h1>
          <p className="auth-shell-description">{description}</p>

          <div className="auth-benefit-list">
            <div>
              <strong>Structured preparation</strong>
              <span>Move from CV review to mock interview in one flow.</span>
            </div>
            <div>
              <strong>Clean practice loop</strong>
              <span>Rehearse, review, refine, and keep improving.</span>
            </div>
          </div>
        </section>

        <section className="auth-panel auth-panel-form">{children}</section>
      </div>
    </div>
  )
}
