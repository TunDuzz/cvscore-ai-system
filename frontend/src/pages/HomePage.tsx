import { AccessSection } from '../components/home/AccessSection'
import { FeaturesSection } from '../components/home/FeaturesSection'
import { HeroSection } from '../components/home/HeroSection'
import { HomeHeader } from '../components/home/HomeHeader'
import { WorkflowSection } from '../components/home/WorkflowSection'
import { PublicLayout } from '../components/layout/PublicLayout'

export function HomePage() {
  return (
    <PublicLayout>
      <HomeHeader />

      <main>
        <HeroSection />
        <FeaturesSection />
        <WorkflowSection />
        <AccessSection />
      </main>
    </PublicLayout>
  )
}
